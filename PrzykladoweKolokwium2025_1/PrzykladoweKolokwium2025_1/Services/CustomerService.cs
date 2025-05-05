using System.Data;
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
            string query = @"Select customer_id, first_name, last_name, 
                        rental_id, rental_date, return_date, name, 
                        price_at_rental, title from Customer 
                        Join Rental on Rental.customer_id = Customer.customer_id
                        Join Status on Status.status_id = Rental.status_id
                        Join Rental_Item on Rental.rental_id = Rental_Item.rental_id
                        Join Movie on Movie.movie_id = Rental_Item.movie_id
                        Where customer_id = @id";
        
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
                                customer.rentals.Find(x => x.id == reader.GetInt32(3)).mowies.Add(new MowieDTO()
                                {
                                    title = reader.GetString(7),
                                    priceAtRental = reader.GetDouble(8),
                                });
                            }
                            else
                            {
                                customer.rentals.Add(new RentalDTO()
                                {
                                    id = reader.GetInt32(3),
                                    rentalDate = reader.GetDateTime(4),
                                    returnDate = reader.GetDateTime(5),
                                    status = reader.GetString(6),
                                    mowies = new List<MowieDTO>()
                                    {
                                        new MowieDTO()
                                        {
                                            title = reader.GetString(7),
                                            priceAtRental = reader.GetDouble(8),
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
                                        returnDate = reader.GetDateTime(5),
                                        status = reader.GetString(6),
                                        mowies = new List<MowieDTO>()
                                        {
                                            new MowieDTO()
                                            {
                                                title = reader.GetString(7),
                                                priceAtRental = reader.GetDouble(8),
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






        public async Task<bool> PostCustomer(int customerId, RentalClientDTO customerDto)
        {
            return false;
        }
        
    }
}