using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IRecersService
{
    Task<RacerParticipationsDTO> GetRacerParticipations(int id);
}