using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface ITripsService
{
    Task<List<TripDTO>> GetTripsAsync();
}