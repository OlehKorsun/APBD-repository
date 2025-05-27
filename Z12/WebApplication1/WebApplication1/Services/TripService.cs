using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
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


    public async Task<bool> PrzypiszKlienta(int idTrip, ClientTripDTO clientTripDto)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var existingTrip = await _context.Trips
                .Include(t => t.ClientTrips)
                .FirstOrDefaultAsync(t => t.IdTrip == idTrip);

            if (existingTrip == null)
                throw new TripNotFound($"Nie istnieje wycieczki o id {idTrip}!");

            if (existingTrip.DateFrom <= DateTime.Now)
                throw new TripDateException("Wycieczka już się rozpoczęła.");

            var existingClient = await _context.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.Pesel == clientTripDto.Pesel);

            if (existingClient != null)
            {
                bool alreadyOnTrip = await _context.ClientTrips.AnyAsync(ct =>
                    ct.IdClient == existingClient.IdClient && ct.IdTrip == idTrip);

                if (alreadyOnTrip)
                    throw new ClientAlreadyOnTrip("Klient jest już zapisany na tę wycieczkę.");
            }

            if (existingClient == null)
            {
                existingClient = new Client
                {
                    FirstName = clientTripDto.FirstName,
                    LastName = clientTripDto.LastName,
                    Email = clientTripDto.Email,
                    Telephone = clientTripDto.Telephone,
                    Pesel = clientTripDto.Pesel
                };
                _context.Clients.Add(existingClient);
                _context.SaveChanges();
            }

            var clientTrip = new ClientTrip
            {
                IdClient = existingClient.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = clientTripDto.PaymentDate
            };

            _context.ClientTrips.Add(clientTrip);
            await _context.SaveChangesAsync();
        
            return true;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}