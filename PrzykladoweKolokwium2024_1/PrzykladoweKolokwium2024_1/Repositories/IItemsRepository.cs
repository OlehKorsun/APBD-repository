namespace PrzykladoweKolokwium2024_1.Repositories;

public interface IItemsRepository
{
    Task<Object> ZrobCosAsync(CancellationToken cancellationToken);
}