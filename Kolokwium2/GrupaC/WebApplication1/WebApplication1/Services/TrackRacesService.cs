using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class TrackRacesService : ITrackRacesService
{
    
    private readonly ApbdDbContext _context;

    public TrackRacesService(ApbdDbContext context)
    {
        _context = context;
    }
    
    public async Task PostRacerParticipationAsync(RacerParticipationRequest participationRequest)
    {
        var race = await _context.Races.FirstOrDefaultAsync(r => r.Name == participationRequest.RaceName);
        if (race == null)
        {
            throw new RaceNotFoundException($"Nie znaleziono kursu o nazwie: {participationRequest.RaceName}");
        }
        
        
        var track = await _context.Tracks.FirstOrDefaultAsync(t => t.Name == participationRequest.TrackName);
        if (track == null)
        {
            throw new TrackNotFoundException($"Nie znaleziono Track o nazwie: {participationRequest.TrackName}");
        }


        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            
            
            foreach (var participation in participationRequest.Participations)
            {
                var racer = await _context.Racers.FindAsync(participation.RacerId);
                if (racer == null)
                {
                    throw new RacerNotFoundException($"Nie znaleziono kierowcy o id: {participation.RacerId}");
                }


                var newTrackRace = new TrackRace()
                {
                    TrackId = track.TrackId,
                    RaceId = race.RaceId,
                    Laps = 1,
                };
                _context.TrackRaces.Add(newTrackRace);
                await _context.SaveChangesAsync();


                var newRaceParticipation = new RaceParticipation()
                {
                    TrackRaceId = newTrackRace.TrackId,
                    RacerId = racer.RacerId,
                    FinishTimeInSeconds = participation.FinishTimeInSeconds,
                    Position = participation.Position,
                };
                _context.RaceParticipations.Add(newRaceParticipation);
                await _context.SaveChangesAsync();

            }


            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}