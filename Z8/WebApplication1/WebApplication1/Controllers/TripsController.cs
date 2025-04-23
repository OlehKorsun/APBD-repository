using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripsService _tripService;

        public TripsController(ITripsService tripService)
        {
            _tripService = tripService;
        }
        
        [HttpGet]
        public async Task<IActionResult> getTrips()
        {
            // zła praktyka
            //var trips = await new TripService.GetTripsAsync();
            
            // dobra praktyka
            var trips = await _tripService.GetTripsAsync();
            return Ok(trips);
        }


        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetTripById(int id)
        // {
        //     if (!await _tripService.DoesTripExists(id))
        //     {
        //         return NotFound();
        //     }
        //
        //     return Ok();
        // }
    }
}
