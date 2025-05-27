using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Exceptions;

namespace WebApplication1.Services;

public class ClientService : IClientService
{
    private readonly ApbdContext _context;

    public ClientService(ApbdContext context)
    {
        _context = context;
    }

    public async Task<bool> DeleteClient(int id)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == id);

        if (client == null)
        {
            throw new ClientNotFound($"Nie istnieje klienta o id: {id}");
        }
        if (client.ClientTrips.Any())
            throw new ClientHasTrips($"Nie udało się usunąć klienta o id: {id}, ponieważ ma przypisane wycieczki!");

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        
        return true;
    }
}