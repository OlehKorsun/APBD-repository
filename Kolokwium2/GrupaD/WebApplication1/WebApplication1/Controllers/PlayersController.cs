using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }


    [HttpGet("{id}/matches")]
    public async Task<IActionResult> GetPlayerMathcesAsync(int id)
    {
        try
        {
            var result = await _playerService.GetPlayerMathcesAsync(id);
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


    [HttpPost]
    public async Task<IActionResult> PostPlayerMathcesAsync([FromBody]PlayerMatchesRequest playerMatchesRequest)
    {
        try
        {
            await _playerService.PostPlayerMatchesAsync(playerMatchesRequest);
            return Created();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}