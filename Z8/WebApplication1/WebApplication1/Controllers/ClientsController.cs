using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using WebApplication1.Models.DTOs;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
    
        private readonly IClientsService _clientsService;

        public ClientsController(IClientsService clientsService)
        {
            _clientsService = clientsService;
        }

        [HttpGet]
        public async Task<IActionResult> getClients()
        {
            var clients = await _clientsService.GetClientsAsync();
            return Ok(clients);
        }

        
        
        
        [HttpGet("{id}/trips")]
        public async Task<IActionResult> getClientTripsByID(int id)
        {
            var trips = await _clientsService.GetClientTripsByClientId(id);
            if (trips == null)
            {
                return NotFound("Nie istnieje Klienta o podanym ID");
            }
            else
            {
                return Ok(trips);
            }
        }


        [HttpPost]
        public async Task<IActionResult> PostClients(ClientCreateDTO client)
        {
            var result = _clientsService.CreateClientAsync(client);
            // Console.WriteLine(result.Result);
            if (result.Result == -1)
            {
                return BadRequest("Problem z danymi wejściowymi");
            }
            return Created("", result.Result);
        }



        [HttpPut("{id}/trips/{tripId}")]
        public async Task<IActionResult> RejestrujKlientaNaWycieczke(int id, int tripId)
        {
            var result = _clientsService.ZarejestrujKlientaNaWycieczke(id, tripId);
            if (!result.Result)
            {
                return Conflict();
            }
            return Ok();
        }
        
        
    }
}

