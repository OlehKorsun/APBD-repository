using Microsoft.Data.SqlClient;
using Poprawka1.Exceptions;
using Poprawka1.Models_DTOs;

namespace Poprawka1.Services;

public class CustomerService : ICustomerService
{
    private readonly string _connectionString;

    public CustomerService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }

    public async Task<CustomerDTO> GetCustomerAsync(int idCustomer)
    {

        CustomerDTO Customer = null;

        var query = @"Select first_name, last_name, ren.rental_id, rental_date, return_date, st.name, title, price_per_day from Customer cust
                        Join Rental ren on cust.customer_id = ren.customer_id
                        Join Status st on st.status_id = ren.status_id
                        Join Rental_Item ri on ri.rental_id = ren.rental_id
                        Join Movie m on m.movie_id = ri.movie_id
                        Where cust.customer_id = @IdCustomer;";
        
        using (SqlConnection connection = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand(query, connection))
        {
            await connection.OpenAsync();
            command.Parameters.AddWithValue("@IdCustomer", idCustomer);

            using (var reader = await command.ExecuteReaderAsync())
            {
                if (!reader.HasRows)
                {
                    throw new CustomerNotFoundException($"Nie znaleziono Customera o ID: {idCustomer}!");
                }
                while (await reader.ReadAsync())
                {
                    if (Customer == null)
                    {
                        Customer = new CustomerDTO()
                        {
                            FirstName = reader.GetString(0),
                            LastName = reader.GetString(1),
                            Rentals = new List<RentalsDTO>()
                        };
                    }

                    var rental = Customer.Rentals.FirstOrDefault(r => r.Id == reader.GetInt32(2));
                    if (rental == null)
                    {
                        rental = new RentalsDTO()
                        {
                            Id = reader.GetInt32(2),
                            RentalDate = reader.GetDateTime(3),
                            ReturnDate = reader.IsDBNull(4) ? (DateTime?)null : reader.GetDateTime(4),
                            Status = reader.GetString(5),
                            Movies = new List<MovieDTO>()
                        };
                        Customer.Rentals.Add(rental);
                    }
                    rental.Movies.Add(new MovieDTO()
                    {
                        Title = reader.GetString(6),
                        PriceAtRental = reader.GetDecimal(7),
                    });
                }
            }
        }
        return Customer;
    }





    public async Task<bool> PostRental(int idCustomer, PostRentalDTO rentalDto)
    {
        var query = @"Select rental_id From Rental Where rental_id=@RentalId";

        int statusId = 0;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            SqlTransaction transaction = connection.BeginTransaction();
            try
            {
                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@RentalId", rentalDto.IdRental);
                    var result = await command.ExecuteScalarAsync();
                    if (result != null)
                    {
                        // await transaction.RollbackAsync();
                        throw new RentalAlreadyExistsException($"Wypożyczenie o Id: {rentalDto.IdRental} już istnieje!");
                    }
                }
                
                query = "Select status_id From Status Where name='Rented'";
                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    statusId = (int)await command.ExecuteScalarAsync();
                }
                
                query = @"Insert Into Rental
                            (rental_id, rental_date, customer_id, status_id) 
                            Values (@RentalId, @RentalDate, @CustomerId, @StatusId);";

                using (SqlCommand command = new SqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@RentalId", rentalDto.IdRental);
                    command.Parameters.AddWithValue("@RentalDate", rentalDto.RentalDate);
                    command.Parameters.AddWithValue("@CustomerId", idCustomer);
                    command.Parameters.AddWithValue("@StatusId", statusId);
                    
                    var result = await command.ExecuteNonQueryAsync();
                    if (result != 1)
                    {
                        // await transaction.RollbackAsync();
                        throw new Exception("Nie udało się stworzyć nowe wypożyczenie!");
                    }
                }
                
                List<PostMovieDTO> existingMovies = new List<PostMovieDTO>();
                query = @"Select movie_id from Movie where title = @Title";
                foreach (var movie in rentalDto.Movies)
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@Title", movie.Title);
                        var result = await command.ExecuteScalarAsync();
                        if (result == null)
                        {
                            // await transaction.RollbackAsync();
                            throw new MovieNotFoundException($"Nie znaleziono filmu o nazwie: {movie.Title}!");
                        }
                        existingMovies.Add(new PostMovieDTO(){ IdMovie = (int)result, PriceAtRental = movie.PriceAtRental });
                    }
                }


                query = @"Insert Into Rental_Item(rental_id, movie_id, price_at_rental) Values (@RentalId, @MovieId, @PriceAtRental);";
                foreach (var movie in existingMovies)
                {
                    using (SqlCommand command = new SqlCommand(query, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@RentalId", rentalDto.IdRental);
                        command.Parameters.AddWithValue("@MovieId", movie.IdMovie);
                        command.Parameters.AddWithValue("@PriceAtRental", movie.PriceAtRental);
                        
                        var result = await command.ExecuteNonQueryAsync();
                        if (result != 1)
                        {
                            // await transaction.RollbackAsync();
                            throw new Exception($"Nie udało się wypożyczyć filmu o id: {movie.IdMovie}!");
                        }
                    }
                }
                await transaction.CommitAsync();
            } 
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Coś poszło nie tak! {ex.Message}");
            }
        }
        return true;
    }
}