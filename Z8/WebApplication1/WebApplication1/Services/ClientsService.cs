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

        
        public async Task<List<ClientTripDTO>> GetClientTripsByClientId(int id)
        {
            var trips = new List<ClientTripDTO>();
            var clientExists = false;
            
            var cmdText = @"select Client.IdClient, Trip.IdTrip, Trip.Name, Description, DateFrom, DateTo, MaxPeople, RegisteredAt, PaymentDate
                            from Client 
                            join Client_Trip on Client.IdClient = Client_Trip.IdClient
                            join Trip on Trip.IdTrip = Client_Trip.IdTrip
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
                        
                        trips.Add(new ClientTripDTO()
                        {
                            IdTrip = reader.GetInt32(1),
                            Name = (reader.GetString(2)),
                            Description = (reader.GetString(3)),
                            DateFrom = reader.GetDateTime(4),
                            DateTo = reader.GetDateTime(5),
                            MaxPeople = reader.GetInt32(6),
                            RegisteredAt = reader.GetInt32(7),
                            PaymentDate = reader.IsDBNull(8)  ? (int?)null : reader.GetInt32(8),
                        });
                    }
                }
            }


            if (!clientExists)
            {
                return null;
            }


            return trips;
        }
        
    }
}
