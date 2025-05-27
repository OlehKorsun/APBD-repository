namespace WebApplication1.Services;

public interface IClientService
{
    Task<bool> DeleteClient(int id);
}