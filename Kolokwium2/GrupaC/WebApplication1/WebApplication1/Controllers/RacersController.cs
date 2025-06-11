using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RacersController : ControllerBase
{
    private readonly IRecersService _service;

    public RacersController(IRecersService service)
    {
        _service = service;
    }

    [HttpGet("{id}/participations")]
    public async Task<IActionResult> GetRacers(int id)
    {
        try
        {
            var result = await _service.GetRacerParticipations(id);
            return Ok(result);
        }
        catch (KeyNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}