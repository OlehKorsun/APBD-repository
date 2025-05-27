using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ITripService
{
    Task<TripsResponse> GetTripsAsync(int? page, int? pageSize);
    Task<bool> PrzypiszKlienta(int idTrip, ClientTripDTO clientTripDto);
}