using WebApplication1.Models.DTOs;

namespace WebApplication1.Services;

public interface IClientsService
{
    Task<List<ClientDTO>> GetClientsAsync();
}