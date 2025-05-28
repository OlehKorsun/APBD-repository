using Poprawka1.Models_DTOs;

namespace Poprawka1.Services;

public interface ICustomerService
{
    Task<CustomerDTO> GetCustomerAsync(int idCustomer);
    Task<bool> PostRental(int idCustomer, PostRentalDTO rentalDto);
}