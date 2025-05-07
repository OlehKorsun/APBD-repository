using PrzykladoweKolokwium2024_1.Repositories;

namespace PrzykladoweKolokwium2024_1.Services;

public class ItemsService : IItemsService
{
    private readonly IItemsRepository _repository;

    
    
    public ItemsService(IItemsRepository repository)
    {
        _repository = repository;
    }
    
    
    
    public async Task<Object> ZrobCosAsync()
    {
        return null;
    }
    
    
    
}