using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication1.Models;

namespace WebApplication1.Services;

public class ProductWarehouseService : IProductWarehouseService
{
    string _connectionString = "Data Source=db-mssql;Initial Catalog=2019SBD;Integrated Security=True;Trust Server Certificate=True";
    public async Task<int> PostProductWarehouse([FromBody] CreateProductWarehouseDTO dto)
    {
        if (dto.Amount < 1)
        {
            throw new ArgumentException("Amount must be greater than 0");
        }

        int IdOrder = 0;

        var query = @"Select 
                        (Select Count(1) From Product Where IdProduct = @IdProduct), 
                        (Select Count(1) From Warehouse Where IdWarehouse = @IdWarehouse),
                        (Select IdOrder From [Order] Where IdProduct = @IdProduct And Amount = @Amount And CreatedAt < @CreatedAt);";
        
        // Punkty 1, 2 oraz 3
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
            cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
            cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
            cmd.Parameters.AddWithValue("@Amount", dto.Amount);

            using (var reader = await cmd.ExecuteReaderAsync())
            {
                if (!await reader.ReadAsync())
                {
                    throw new Exception("No records found");
                }
                if (reader.GetInt32(0) == 0)
                {
                    throw new Exception("Nie znaleziono produktu");
                }
                if (reader.GetInt32(1) == 0)
                {
                    throw new Exception("Nie znaleziono magazynu");
                }

                if (reader.IsDBNull(2))
                {
                    throw new Exception("Nie znaleziono zamówienia");
                }
                
                IdOrder = reader.GetInt32(2);

                // if (reader.GetInt32(3) == 0)
                // {
                //     throw new Exception("Zamówienie zostało przypadkiem zrealizowane");
                // }
            }
        }

        

        Console.WriteLine("Jestem tu!!!! -=-=-=-=-=-=-=-=-=-=-=");
        DateTime dateTime = DateTime.Now;
        Console.WriteLine(dateTime);
        query = @"Update [Order] Set FulfilledAt = @DateTime Where IdOrder = @IdOrder;";
        
        // Punkt 4
        using(SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@DateTime", dateTime);
            cmd.Parameters.AddWithValue("@IdOrder", IdOrder);

            var result = await cmd.ExecuteNonQueryAsync();

            if (result == 0)
            {
                throw new Exception("Nie udało się zaaktualizować czasu FulfilledAt");
            }

        }


        int newId = 0;
        // // Punkt 5
        //
        // // Znalezienie nowego Id dla Product_Warehouse
        // query = "Select Max(IdProductWarehouse)+1 From Product_Warehouse;";
        // using (SqlConnection conn = new SqlConnection(_connectionString))
        // using (SqlCommand cmd = new SqlCommand(query, conn))
        // {
        //     await conn.OpenAsync();
        //     var res = await cmd.ExecuteScalarAsync();
        //     if (res == null || res == DBNull.Value)
        //     {
        //         newId = 1;
        //         //throw new Exception("Problem ze znalezieniem nowego idProductWarehouse");
        //     }
        //     else
        //     {
        //         newId = Convert.ToInt32(res);
        //     }
        //     
        // }

        decimal price = 0;
        
        // Znalezienie ceny produktu
        query = @"Select Price From Product Where IdProduct = @IdProduct;";
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
            var res = await cmd.ExecuteScalarAsync();
            if (res == null)
            {
                throw new Exception("Problem ze znalezieniem ceny");
            }
            
            price = Convert.ToDecimal(res);
        }
        
        
        query = @"Insert Into Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                Values (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);
                Select Scope_Identity();";
        
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand(query, conn))
        {
            await conn.OpenAsync();
            // cmd.Parameters.AddWithValue("@IdProductWarehouse", newId);
            cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
            cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
            cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
            cmd.Parameters.AddWithValue("@Amount", dto.Amount);
            cmd.Parameters.AddWithValue("@Price", price*dto.Amount);
            cmd.Parameters.AddWithValue("@CreatedAt", dateTime);
            
            var res = await cmd.ExecuteScalarAsync();
            if (res == null)
            {
                throw new Exception("Nie udało się dodać rekord do Product_Warehouse");
            }
            newId = Convert.ToInt32(res);
        }
        return newId;
    }
}