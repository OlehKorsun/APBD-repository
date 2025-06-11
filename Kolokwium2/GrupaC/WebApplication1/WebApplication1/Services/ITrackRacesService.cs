using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface ITrackRacesService
{
    Task PostRacerParticipationAsync(RacerParticipationRequest participationRequest);
}