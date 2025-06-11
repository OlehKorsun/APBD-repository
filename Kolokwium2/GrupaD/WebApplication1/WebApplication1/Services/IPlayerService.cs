using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IPlayerService
{
    Task<PlayerMatchesDTO> GetPlayerMathcesAsync(int playerId);

    Task PostPlayerMatchesAsync(PlayerMatchesRequest playerMatchesRequest);
}