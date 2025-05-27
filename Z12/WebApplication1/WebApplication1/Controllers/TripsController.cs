using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int? page, [FromQuery] int? pageSize)
    {
        var res = await _tripService.GetTripsAsync(page, pageSize);
        return Ok(res);
    }


    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> PrzypiszKlienta(int idTrip, [FromBody]ClientTripDTO clientTripDto)
    {
        try
        {
            var result =await  _tripService.PrzypiszKlienta(idTrip, clientTripDto);
            return Ok("Klient został przypisany do wycieczki");
        }
        catch (ClientHasAlreadyExists e)
        {
            return Conflict(e.Message);
        }
        catch (TripDateException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}