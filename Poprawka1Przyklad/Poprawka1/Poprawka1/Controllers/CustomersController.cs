using Microsoft.AspNetCore.Mvc;
using Poprawka1.Exceptions;
using Poprawka1.Models_DTOs;
using Poprawka1.Services;

namespace Poprawka1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomersController(ICustomerService customerService)
    {
        _customerService = customerService;
    }


    [HttpGet("{idCustomer}/rentals")]
    public async Task<IActionResult> GetCustomers(int idCustomer)
    {
        try
        {
            var result = await _customerService.GetCustomerAsync(idCustomer);
            return Ok(result);
        }
        catch (CustomerNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [HttpPost("{idCustomer}/rentals")]
    public async Task<IActionResult> PostRentals(int idCustomer, [FromBody]PostRentalDTO rental)
    {
        try
        {
            var result = await _customerService.PostRental(idCustomer, rental);
            return Ok("Wypożyczono filmy!");
        }
        catch (RentalAlreadyExistsException e)
        {
            return Conflict(e.Message);
        }
        catch (CustomerNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (MovieNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}