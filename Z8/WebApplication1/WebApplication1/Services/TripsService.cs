using Microsoft.Data.SqlClient;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public class TripsService : ITripsService
{
    private string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    
    // Endpoint 1
    public async Task<List<TripDTO>> GetTripsAsync()   // połączyłem się do lokalnej bazy o nazwie 2019SBD
    {
        var trips = new List<TripDTO>();
        
        var cmdText = @"select Trip.IdTrip, Trip.Name, Description, DateFrom, DateTo, MaxPeople, Country.Name
                        from Trip 
                        join Country_Trip on Trip.IdTrip = Country_Trip.IdTrip
                        join Country on Country.IdCountry = Country_Trip.IdCountry";

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
                    if (trips.Exists(t => t.IdTrip == reader.GetInt32(idTripOrdinal)))
                    {
                        trips.Find(t => t.IdTrip == reader.GetInt32(idTripOrdinal)).Countries
                            .Add(new CountryDTO(reader.GetString(6)));
                    }
                    else
                    {
                        TripDTO tripDto = new TripDTO()
                        {
                            IdTrip = reader.GetInt32(idTripOrdinal),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            DateFrom = reader.GetDateTime(3),
                            DateTo = reader.GetDateTime(4),
                            MaxPeople = reader.GetInt32(5)
                        };
                        tripDto.Countries.Add(new CountryDTO(reader.GetString(6)));
                    trips.Add(tripDto);
                    }

                    
                }
            }
            
            
        }
        
        
        return trips;
    }
    
    
    // tutaj nowa metoda, na nowo się łączymy i robimy coś
}