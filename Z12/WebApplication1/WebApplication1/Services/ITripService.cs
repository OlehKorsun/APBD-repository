using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ITripService
{
    Task<IEnumerable<Trip>> GetTripsAsync();
}