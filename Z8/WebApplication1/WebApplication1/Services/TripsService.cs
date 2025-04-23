using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class TripsService : ITripsService
{
    private string _connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=APBD;Integrated Security=True;";
    public async Task<List<TripDTO>> GetTripsAsync()   // połączyłem się do lokalnej bazy o nazwie APBD
    {
        var trips = new List<TripDTO>();
        
        var cmdText = @"select IdTrip, Name from Trip";

        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(cmdText, conn))
        {
            // otwiera połączenie
            await conn.OpenAsync();

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                int idTripOrdinal = reader.GetOrdinal("IdTrip");
                while (await reader.ReadAsync())
                {
                    
                    trips.Add(new TripDTO()
                    {
                        IdTrip = reader.GetInt32(idTripOrdinal),   // argument - numer kolumny, można szukać automatycznie po nazwie kolumny
                        Name = reader.GetString(1),
                    });
                }
            }
            
            
        }
        
        
        return trips;
    }
    
    
    // tutaj nowa metoda, na nowo się łączymy i robimy coś
}