using WebApplication1.DTOs;

namespace WebApplication1.Services;

public interface IClientService
{
    Task AddOrder(int clientId, OrderRequest order);
}