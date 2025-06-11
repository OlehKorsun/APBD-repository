using WebApplication1.DTOs;
using WebApplication1.Models;

namespace WebApplication1.Services;

public interface ICustomerService
{
    Task<CustomerDTO> GetCustomerByIdAsync(int id);
    Task PostCustomerAsync(CustomerTicketsRequest customerRequest);
}