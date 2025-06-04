using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTOs;
using WebApplication1.Exceptions;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly IClientService clientService;

    public ClientsController(IClientService clientService)
    {
        this.clientService = clientService;
    }

    [HttpPost("{id}/orders")]
    public async Task<IActionResult> PostOrder(int id, [FromBody]OrderRequest order)
    {
        try
        {
            await clientService.AddOrder(id, order);
            return Created();
        }
        catch (ClientNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (EmployeeNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (ProductNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}