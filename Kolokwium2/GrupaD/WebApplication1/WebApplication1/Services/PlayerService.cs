using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class PlayerService : IPlayerService
{
    private readonly ApbdDbContext _context;

    public PlayerService(ApbdDbContext context)
    {
        _context = context;
    }


    public async Task<PlayerMatchesDTO> GetPlayerMathcesAsync(int playerId)
    {
        
        var player = await _context.Players.FindAsync(playerId);

        if (player == null)
        {
            throw new KeyNotFoundException($"Nie znaleziono gracza z id: {playerId}");
        }

        var matches = await _context.Players
            .Include(pm => pm.PlayerMatches)
            .Include(pm => pm.PlayerMatches)
                .ThenInclude(m => m.Match)
            .Include(pm => pm.PlayerMatches)
                .ThenInclude(m => m.Match)
                .ThenInclude(t => t.Tournament)
            .Include(pm => pm.PlayerMatches)
                .ThenInclude(m => m.Match)
                .ThenInclude(m => m.Map)
            .ToListAsync();

        var result = matches.Select(a => new PlayerMatchesDTO
            {
                PlayerId = a.PlayerId,
                BirthDate = a.BirthDate,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Matches = a.PlayerMatches.Select(m => new MatchDTO()
                {
                    Map = m.Match.Map.Name,
                    Tournament = m.Match.Tournament.Name,
                    Date = m.Match.MatchDate,
                    MVPs = m.MVPs,
                    Rating = m.Rating,
                    Team1Score = m.Match.Team1Score,
                    Team2Score = m.Match.Team2Score,
                }).ToList(),
            }).FirstOrDefault(p => p.PlayerId == playerId);

        if (result == null)
        {
            return new PlayerMatchesDTO()
            {
                PlayerId = player.PlayerId,
                BirthDate = player.BirthDate,
                FirstName = player.FirstName,
                LastName = player.LastName,
                Matches = new List<MatchDTO>(),
            };
        }
        
        return result;
    }


    public async Task PostPlayerMatchesAsync(PlayerMatchesRequest playerMatchesRequest)
    {
        
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var player = new Player()
            {
                FirstName = playerMatchesRequest.FirstName,
                LastName = playerMatchesRequest.LastName,
                BirthDate = playerMatchesRequest.BirthDate,
            };
            await _context.Players.AddAsync(player);
            await _context.SaveChangesAsync();
            
            foreach (var playerMatch in playerMatchesRequest.Matches)
            {
                var match = await _context.Matches.FindAsync(playerMatch.MatchId);
                if (match == null)
                {
                    throw new KeyNotFoundException($"Nie znaleziono meczu o id: {playerMatch.MatchId}");
                }

                var newPlayerMatch = new PlayerMatch()
                {
                    MatchId = playerMatch.MatchId,
                    PlayerId = player.PlayerId,
                    MVPs = playerMatch.MVPs,
                    Rating = playerMatch.Rating,
                };
                await _context.PlayerMatches.AddAsync(newPlayerMatch);

                if (match.BestRating < playerMatch.Rating)
                {
                    match.BestRating = playerMatch.Rating;
                }
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