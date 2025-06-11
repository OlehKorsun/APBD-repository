using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("track-races")]
public class TrackRacesController : ControllerBase
{
    private readonly ITrackRacesService _trackRacesService;

    public TrackRacesController(ITrackRacesService trackRacesService)
    {
        _trackRacesService = trackRacesService;
    }

    public async Task<IActionResult> PostParticipationAsync([FromBody]RacerParticipationRequest participationRequest)
    {
        try
        {
            await _trackRacesService.PostRacerParticipationAsync(participationRequest);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
        
    }
}