using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[Route("/api/[controller]")]
[ApiController]
public class WarehouseController : ControllerBase
{
    private readonly IProductWarehouseService _productWarehouseService;

    public WarehouseController(IProductWarehouseService productWarehouseService)
    {
        _productWarehouseService = productWarehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> PostProductWarehouse([FromBody] CreateProductWarehouseDTO dto)
    {
        try
        {
            var result = _productWarehouseService.PostProductWarehouseProcedure(dto);
            return Ok(result.Result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}