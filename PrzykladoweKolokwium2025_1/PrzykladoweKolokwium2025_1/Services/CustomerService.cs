using Microsoft.Data.SqlClient;
using PrzykladoweKolokwium2025_1.Models;

namespace PrzykladoweKolokwium2025_1.Services
{
    public class CustomerService : ICustomerService
    {
        private string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
        public async Task<CustomerDTO> GetCustomersAsync(int id)
        {
            CustomerDTO? customer = null;
            var customerExists = false;
            string query = @"Select Customer.customer_id, first_name, last_name, 
                        Rental.rental_id, rental_date, return_date, name, 
                        price_at_rental, title from Customer 
                        Join Rental on Rental.customer_id = Customer.customer_id
                        Join Status on Status.status_id = Rental.status_id
                        Join Rental_Item on Rental.rental_id = Rental_Item.rental_id
                        Join Movie on Movie.movie_id = Rental_Item.movie_id
						Where Customer.customer_id = @id";
        
            using(SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        if (customer != null)
                        {
                            if (customer.rentals.Exists(x => x.id == reader.GetInt32(3)))
                            {
                                customer.rentals.Find(x => x.id == reader.GetInt32(3))
                                    .movies.Add(new MowieDTO()
                                {
                                    title = reader.GetString(8),
                                    rentalPrice = reader.GetDecimal(7),
                                });
                            }
                            else
                            {
                                customer.rentals.Add(new RentalDTO()
                                {
                                    id = reader.GetInt32(3),
                                    rentalDate = reader.GetDateTime(4),
                                    returnDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                    status = reader.GetString(6),
                                    movies = new List<MowieDTO>()
                                    {
                                        new MowieDTO()
                                        {
                                            title = reader.GetString(8),
                                            rentalPrice = reader.GetDecimal(7),
                                        }
                                    }
                                });
                            }
                        }
                        else
                        {
                            customer = new CustomerDTO()
                            {
                                customer_id = reader.GetInt32(0),
                                first_name = reader.GetString(1),
                                last_name = reader.GetString(2),
                                rentals = new List<RentalDTO>()
                                {
                                    new RentalDTO()
                                    {
                                        id = reader.GetInt32(3),
                                        rentalDate = reader.GetDateTime(4),
                                        returnDate = reader.IsDBNull(5) ? (DateTime?)null : reader.GetDateTime(5),
                                        status = reader.GetString(6),
                                        movies = new List<MowieDTO>()
                                        {
                                            new MowieDTO()
                                            {
                                                title = reader.GetString(8),
                                                rentalPrice = reader.GetDecimal(7),
                                            }
                                        }
                                    }
                                }
                            };
                        }
                    }
                }
            }
            return customer;
        }






        public async Task<bool> PostCustomerAsync(int customerId, RentalClientDTO customerDto)
        {
            
            Console.WriteLine(customerId);
            foreach (var VARIABLE in customerDto.movies)
            {
                Console.WriteLine("Movies");
                Console.WriteLine(VARIABLE.title);
                Console.WriteLine(VARIABLE.rentalPrice);
                Console.WriteLine("Movies");
            }
            
            Console.WriteLine(customerDto.rentalDate);
            Console.WriteLine(customerDto.id);



            int statusId = -1;
            List<int> mowiesIds = new List<int>();
            Dictionary<int, MowieDTO> mowiesMap = new Dictionary<int, MowieDTO>();
            
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                SqlTransaction transaction = connection.BeginTransaction();
                
                try
                {
                    // 1 sprawdź czy klient istnieje
                    string checkCustomerQuery = @"Select 1 From Customer Where customer_id = @customerId";

                    using (SqlCommand cmd = new SqlCommand(checkCustomerQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        var rowsAffected = await cmd.ExecuteScalarAsync();
                        Console.WriteLine("Customer ID: " + customerId);
                        Console.WriteLine("Rows affected: " + rowsAffected);
                        if (rowsAffected == null)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    
                    // 2 sprawdzanie czy istnieją filmy i pobieranie ich id
                    string checkMoviesQuery = @"Select movie_id From Movie Where title = @title";
                    foreach(MowieDTO movie in customerDto.movies)
                    {
                        using (SqlCommand cmd = new SqlCommand(checkMoviesQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@title", movie.title);
                            var result = await cmd.ExecuteScalarAsync();
                            Console.WriteLine("Result: " + result);
                            if (result == null)
                            {
                                transaction.Rollback();
                                return false;
                            }
                            mowiesIds.Add((int)result);
                            mowiesMap.Add((int)result, movie);
                        }
                    }
                    
                    
                    // 3 pobieranie id statusu
                    string statusQuery = "Select status_id from Status Where name = 'Rented'";
                    using (SqlCommand cmd = new SqlCommand(statusQuery, connection, transaction))
                    {
                        var result = await cmd.ExecuteScalarAsync();
                        Console.WriteLine("Resultat: " + result);
                        if (result == null)
                        {
                            transaction.Rollback();
                            return false;
                        }
                        statusId = Convert.ToInt32(result);
                    }
                    
                    
                    // 4 wypożyczenie filmu
                    // 4.1 dodanie rekordu do tablicy Rent
                    string rentQuery = @"Insert Into Rental(rental_id, rental_date, customer_id, status_id) Values (@rental_id, @rentalDate, @customerId, @statusId);";
                    Console.WriteLine("4.1");
                    using (SqlCommand cmd = new SqlCommand(rentQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@rental_id", customerDto.id);
                        cmd.Parameters.AddWithValue("@rentalDate", customerDto.rentalDate);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@statusId", statusId);
                        
                        var result = await cmd.ExecuteNonQueryAsync();
                        Console.WriteLine("Result: " + result);
                        if (result == 0)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                    
                    // 4.2 dodanie rekordów do tablicy Rental_Item
                    Console.WriteLine("4.2");
                    string rentalItemQuery = @"Insert Into Rental_Item(rental_id, movie_id, price_at_rental) Values (@rental_id, @movie_id, @price_at_rental)";
                    foreach (int mowieId in mowiesIds)
                    {
                        using (SqlCommand cmd = new SqlCommand(rentalItemQuery, connection, transaction))
                        {
                            cmd.Parameters.AddWithValue("@rental_id", customerDto.id);
                            cmd.Parameters.AddWithValue("@movie_id", mowieId);
                            cmd.Parameters.AddWithValue("@price_at_rental", mowiesMap.GetValueOrDefault(mowieId).rentalPrice);

                            var result = await cmd.ExecuteNonQueryAsync();
                            Console.WriteLine("Result 4,2: " + result);
                            if (result != 1)
                            {
                                transaction.Rollback();
                                return false;
                            }
                        }
                    }
                    Console.WriteLine("koniec");
                    transaction.Commit();
                    Console.WriteLine("koniec");
                    return true;
                }
                catch(Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        
        
        
    }
}