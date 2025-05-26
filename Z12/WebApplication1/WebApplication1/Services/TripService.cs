using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TripService : ITripService
{
    
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Trip>> GetTripsAsync()
    {
        return _context.Trips;
    }
}