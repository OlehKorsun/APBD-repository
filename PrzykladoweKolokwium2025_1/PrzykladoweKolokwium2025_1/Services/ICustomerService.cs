using PrzykladoweKolokwium2025_1.Models;

namespace PrzykladoweKolokwium2025_1.Services;

public interface ICustomerService
{
    Task<CustomerDTO> GetCustomersAsync(int id);
    Task<bool> PostCustomer(int customerId, RentalClientDTO customerDto);
}