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
        
    }
}
