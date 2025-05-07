using PrzykladoweKolokwium2024_1.Services;

namespace PrzykladoweKolokwium2024_1.Repositories;

public class ItemsRepository : IItemsRepository
{
    private readonly string _connectionString;

    public ItemsRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }
    
    public async Task<Object> ZrobCosAsync(CancellationToken cancellationToken)
    {
        // komunikacja z bazą danych
        return null;
    }
    
}