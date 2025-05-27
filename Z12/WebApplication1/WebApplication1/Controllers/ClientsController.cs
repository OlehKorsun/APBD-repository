using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }


    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteTrip(int idClient)
    {
        try
        {
            await _clientService.DeleteClient(idClient);
            return Ok($"Udało się poprawnie usunąć klienta o id: {idClient}");
        }
        catch (ClientHasTrips e)
        {
            return BadRequest(e.Message);
        }
        catch (ClientNotFound e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}