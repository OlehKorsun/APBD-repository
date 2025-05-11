using WebApplication1.Models;

namespace WebApplication1.Services;

public interface IProductWarehouseService
{
    Task<int> PostProductWarehouse(CreateProductWarehouseDTO dto);
    
    Task<int> PostProductWarehouseProcedure(CreateProductWarehouseDTO dto);
}