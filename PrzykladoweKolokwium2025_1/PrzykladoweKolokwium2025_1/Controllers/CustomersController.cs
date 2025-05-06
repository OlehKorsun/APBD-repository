using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolokwium2025_1.Models;
using PrzykladoweKolokwium2025_1.Services;

namespace PrzykladoweKolokwium2025_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet("{customerId}/rentals")]
        public async Task<IActionResult> GetCustomersAsync(int customerId)
        {
            var customers = await _customerService.GetCustomersAsync(customerId);
            if (customers == null)
            {
                return NotFound("Nie istnieje klienta o podanym ID");
            }
            return Ok(customers);
        }


        [HttpPost("{customerId}/rentals")]
        public async Task<IActionResult> PostCustomerAsync(int customerId, RentalClientDTO customerDto)
        {
            var result = _customerService.PostCustomer(customerId, customerDto);

            if (!result.Result)
            {
                return BadRequest("Nie udało się wyporzyczyć film");
            }
            return Ok(result);
        }
    }
}

