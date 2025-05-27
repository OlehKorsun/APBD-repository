using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TripService : ITripService
{
    
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }
    
    public async Task<TripsResponse> GetTripsAsync(int? page, int? pageSize)
    {
        
        int currentPage = page ?? 1;
        int currentPageSize = pageSize ?? 10;
        
        var totalCount = await _context.Trips.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)currentPageSize);
        
        var trips = await _context.Trips
            .Include(b => b.IdCountries)
            .Include(a => a.ClientTrips)
                .ThenInclude(c => c.IdClientNavigation)
            .OrderBy(t => t.DateFrom)
            .Skip((currentPage - 1) * currentPageSize)
            .Take(currentPageSize)
            .ToListAsync();
        
                
        var tripDto = trips.Select(t => new TripDTO()
        {
            Name = t.Name,
            Description = t.Description,
            DateFrom = t.DateFrom,
            DateTo = t.DateTo,
            MaxPeople = t.MaxPeople,
            Countries = t.IdCountries.Select(c => new CountryDTO()
            {
                Name = c.Name,
            }).ToList(),
            Clients = t.ClientTrips.Select(c => new ClientDTO()
            {
                FirstName = c.IdClientNavigation.FirstName,
                LastName = c.IdClientNavigation.LastName,
            }).ToList()
        }).ToList();

        var result = new TripsResponse
        {
            PageNum = currentPage,
            PageSize = currentPageSize,
            AllPages = totalPages,
            Trips = tripDto
        };
        
        return result;
    }
}