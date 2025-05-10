using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProductWarehouseService : IProductWarehouseService
{
    string connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    public async Task<int> PostProductWarehouse([FromBody] CreateProductWarehouseDTO dto)
    {
        if (dto.Amount < 1)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        var query = @"Select 
                        (Select Count(1) From Product Where IdProduct = @IdProduct), 
                        (Select Count(1) From Warehouse Where IdWarehouse = @IdWarehouse)";
        
        
        
        
        
        
        return 0;
    }
}