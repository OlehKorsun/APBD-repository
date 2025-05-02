using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services
{
    
    public class ClientsService : IClientsService
    {
        private string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";


        public async Task<List<ClientDTO>> GetClientsAsync()
        {
            var clients = new List<ClientDTO>();
            
            var cmdText = @"select IdClient, FirstName, LastName, Email, Telephone, Pesel from Client";
            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    // int idTripOrdinal = reader.GetOrdinal("IdTrip");
                    while (await reader.ReadAsync())
                    {
                    
                        clients.Add(new ClientDTO()
                        {
                            IdClient = reader.GetInt32(0),
                            // IdClient = reader.GetInt32(idTripOrdinal),   // argument - numer kolumny, można szukać automatycznie po nazwie kolumny
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Email = reader.GetString(3),
                            Telephone = reader.GetString(4),
                            Pesel = reader.GetString(5),
                        });
                    }
                }
            }
            return clients;
        }

        
        
        
        
        // Endpoint 2
        public async Task<List<ClientTripDTO>> GetClientTripsByClientId(int id)
        {
            var trips = new List<ClientTripDTO>();
            var clientExists = false;
            
            var cmdText = @"select Client.IdClient, Trip.IdTrip, Trip.Name, Description, DateFrom, 
                            DateTo, MaxPeople, RegisteredAt, PaymentDate, Country.Name
                            from Client 
                            join Client_Trip on Client.IdClient = Client_Trip.IdClient
                            join Trip on Trip.IdTrip = Client_Trip.IdTrip
                            join Country_Trip on Trip.IdTrip = Country_Trip.IdTrip
                            join Country on Country.IdCountry = Country_Trip.IdCountry
                            where Client.IdClient = @id;";
            
            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(cmdText, conn))
            {
                await conn.OpenAsync();
                cmd.Parameters.AddWithValue("@id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clientExists = true;
                        if (reader["IdTrip"] == DBNull.Value)
                            continue;

                        if (trips.Exists(t => t.IdTrip == reader.GetInt32(1)))
                        {
                            trips.Find(t => t.IdTrip == reader.GetInt32(1)).Countries.Add(new CountryDTO(reader.GetString(9)));
                        }
                        else
                        {
                            var trip = new ClientTripDTO()
                            {
                                IdTrip = reader.GetInt32(1),
                                Name = (reader.GetString(2)),
                                Description = (reader.GetString(3)),
                                DateFrom = reader.GetDateTime(4),
                                DateTo = reader.GetDateTime(5),
                                MaxPeople = reader.GetInt32(6),
                                RegisteredAt = reader.GetInt32(7),
                                PaymentDate = reader.IsDBNull(8) ? (int?)null : reader.GetInt32(8),
                            };
                            trip.Countries.Add(new CountryDTO(reader.GetString(9)));
                            trips.Add(trip);
                        }
                    }
                }
            }
            if (!clientExists)
            {
                return null;
            }
            return trips;
        }




        
        
        
        // Endpoint 3
        public async Task<int> CreateClientAsync([FromBody]ClientCreateDTO client)
        {
            
            // Jeśli problem z danymi wejściowymi zwracam -1
            if (string.IsNullOrEmpty(client.FirstName) || string.IsNullOrEmpty(client.LastName) ||
                string.IsNullOrEmpty(client.Pesel) || string.IsNullOrEmpty(client.Email) || string.IsNullOrEmpty(client.Telephone) ||
                client.Pesel.Length!=11)
            {
                return -1;
            }

            string query = @"Insert Into Client(FirstName, LastName, Email, Telephone, Pesel) 
            Values (@FirstName, @LastName, @Email, @Telephone, @Pesel);
            Select Scope_Identity();";
            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@FirstName", client.FirstName);
                cmd.Parameters.AddWithValue("@LastName", client.LastName);
                cmd.Parameters.AddWithValue("@Email", client.Email);
                cmd.Parameters.AddWithValue("@Telephone", client.Telephone);
                cmd.Parameters.AddWithValue("@Pesel", client.Pesel);
                await conn.OpenAsync();
                
                var result = await cmd.ExecuteScalarAsync();
                return Convert.ToInt32(result);
                // return CreatedAtAction(nameof(GetClientsAsync), new { id = result }, client);
            }
           
        }






        // Endpoint 4
        public async Task<bool> ZarejestrujKlientaNaWycieczke(int id, int tripId)
        {
            string checkQuery = @"Select 
                                (Select Count(1) From Client Where IdClient = @id) AS CzyIstniejeClient
                                (Select Count(1) From Trip Where IdTrip = @tripId) AS CzyIstniejeTrip
                                (Select Count(1) From Client_Trip Where IdTrip = @tripId) AS ZapisanoClientow
                                (Select MaxPeople from Trip Where IdTrip = @tripId) AS MaxPeople,
                                (SELECT COUNT(1) FROM Client_Trip WHERE IdClient = @id AND IdTrip = @idTrip) AS CzyJestJuzZapisany;";
            
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@tripId", tripId);
                await conn.OpenAsync();

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    await reader.ReadAsync();
                    var CzyIstniejeClient = reader.GetInt32(0);
                    var CzyIstniejeTrip = reader.GetInt32(1);
                    var ZapisanoClientow = reader.GetInt32(2);
                    var MaxPeople = reader.GetInt32(3);
                    var CzyJestJuzZapisany = reader.GetInt32(4);
                    
                    // Sprawdzam czy istnieją client i wycieczka oraz czy nie przekroczono limitu oraz czy client nie jest jeszcze zapisany na tą wycieczkę
                    if (CzyIstniejeClient == 0 || CzyIstniejeTrip == 0 || ZapisanoClientow >= MaxPeople || CzyJestJuzZapisany == 1)
                    {
                        return false;
                    }
                }
            }

            // Wstawiam do bazy danych nowy rekord
            var data = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            string insertQuery = @"INSERT INTO CLIENT_TRIP(IdClient, IdTrip, RegisteredAt) VALUES (@id, @idTrip, @data) ";
            
            using(SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@idTrip", tripId);
                cmd.Parameters.AddWithValue("@data", data);
                await conn.OpenAsync();
                var result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
        
        
        
    }
}
