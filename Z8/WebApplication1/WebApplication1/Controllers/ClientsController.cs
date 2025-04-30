using Microsoft.AspNetCore.Mvc;
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
        
        
        
        
    }
}

