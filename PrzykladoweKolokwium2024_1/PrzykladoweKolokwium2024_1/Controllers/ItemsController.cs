using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium2024_1.Services;

namespace PrzykladoweKolokwium2024_1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ItemsService _itemsService;

    public ItemsController(IItemsService service)
    {
        
    }
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        
        var result = _itemsService.ZrobCosAsync();
        return Ok(result);
    }
}