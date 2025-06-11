using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

namespace WebApplication1.Services;

public class RecersService : IRecersService
{
    
    private readonly ApbdDbContext _context;

    public RecersService(ApbdDbContext context)
    {
        _context = context;
    }
    
    public async Task<RacerParticipationsDTO> GetRacerParticipations(int id)
    {
        var racers = await _context.Racers
            .Include(r => r.RaceParticipations)
            .Include(r => r.RaceParticipations)
                .ThenInclude(t => t.TrackRace)
            .Include(r => r.RaceParticipations)
                .ThenInclude(t => t.TrackRace)
                .ThenInclude(t => t.Track)
            .Include(r => r.RaceParticipations)
                .ThenInclude(t => t.TrackRace)
                .ThenInclude(t => t.Race)
            .Where(r => r.RacerId == id)
            .ToListAsync();

        if (racers.Count == 0)
        {
            throw new KeyNotFoundException($"Nie znaleziono kursów albo nie istnieje kierowcy o id: {id}");
        }

        var res = racers.Select(e => new RacerParticipationsDTO
        {
            RacerId = e.RacerId,
            FirstName = e.FirstName,
            LastName = e.LastName,
            Participations = e.RaceParticipations.Select(p => new ParticipationsDTO()
            {
                Position = p.Position,
                FinishTimeInSeconds = p.FinishTimeInSeconds,
                Laps = p.Position,
                Race = new RaceDTO()
                {
                    Date = p.TrackRace.Race.Date,
                    Name = p.TrackRace.Race.Name,
                    Location = p.TrackRace.Race.Location,
                },
                Track = new TrackDTO()
                {
                    LengthInKm = p.TrackRace.Track.LengthInKm,
                    Name = p.TrackRace.Track.Name,
                }
            }).ToList(),
        });
        
        return res.FirstOrDefault();
    }
}