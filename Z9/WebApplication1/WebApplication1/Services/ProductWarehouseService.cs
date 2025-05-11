using System.Data;
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
        int newId = 0;
        decimal price = 0;
        DateTime dateTime = DateTime.Now;
        
        

        var query = @"Select 
                        (Select Count(1) From Product Where IdProduct = @IdProduct), 
                        (Select Count(1) From Warehouse Where IdWarehouse = @IdWarehouse),
                        (Select IdOrder From [Order] Where IdProduct = @IdProduct And Amount = @Amount And CreatedAt < @CreatedAt);";
        
        // Punkty 1, 2 oraz 3
        using (SqlConnection conn = new SqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            SqlTransaction transaction = conn.BeginTransaction();
            try
            {
                using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
                cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
                cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
                cmd.Parameters.AddWithValue("@Amount", dto.Amount);

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (!await reader.ReadAsync())
                    {
                        transaction.Rollback();
                        throw new Exception("No records found");
                    }
                    if (reader.GetInt32(0) == 0)
                    {
                        transaction.Rollback();
                        throw new Exception("Nie znaleziono produktu");
                    }
                    if (reader.GetInt32(1) == 0)
                    {
                        transaction.Rollback();
                        throw new Exception("Nie znaleziono magazynu");
                    }
                    if (reader.IsDBNull(2))
                    {
                        transaction.Rollback();
                        throw new Exception("Nie znaleziono zamówienia");
                    }
                    IdOrder = reader.GetInt32(2);
                }
            }
            
            
            
            
            
            
            query = @"Update [Order] Set FulfilledAt = @DateTime Where IdOrder = @IdOrder;";
            
            // Punkt 4
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@DateTime", dateTime);
                cmd.Parameters.AddWithValue("@IdOrder", IdOrder);

                var result = await cmd.ExecuteNonQueryAsync();

                if (result == 0)
                {
                    transaction.Rollback();
                    throw new Exception("Nie udało się zaaktualizować czasu FulfilledAt");
                }
            }
            
            
            
            
            
            
            // Punkt 5
        
            // Znalezienie ceny produktu
            query = @"Select Price From Product Where IdProduct = @IdProduct;";
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
                var res = await cmd.ExecuteScalarAsync();
                if (res == null)
                {
                    transaction.Rollback();
                    throw new Exception("Problem ze znalezieniem ceny");
                }
                price = Convert.ToDecimal(res);
            }
            
            
            
            
            
            
            
            query = @"Insert Into Product_Warehouse (IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt)
                Values (@IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);
                Select Scope_Identity();";
            
            using (SqlCommand cmd = new SqlCommand(query, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
                cmd.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
                cmd.Parameters.AddWithValue("@IdOrder", IdOrder);
                cmd.Parameters.AddWithValue("@Amount", dto.Amount);
                cmd.Parameters.AddWithValue("@Price", price*dto.Amount);
                cmd.Parameters.AddWithValue("@CreatedAt", dateTime);
            
                var res = await cmd.ExecuteScalarAsync();
                if (res == null)
                {
                    transaction.Rollback();
                    throw new Exception("Nie udało się dodać rekord do Product_Warehouse");
                }
                newId = Convert.ToInt32(res);
            }
            transaction.Commit();
            return newId;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Oj, coś poszło nie tak :_)");
            }
        }
    }







    public async Task<int> PostProductWarehouseProcedure(CreateProductWarehouseDTO dto)
    {
        int res = 0;
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand command = new SqlCommand("AddProductToWarehouse", conn))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@IdProduct", dto.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", dto.IdWarehouse);
            command.Parameters.AddWithValue("@Amount", dto.Amount);
            command.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt);
            
            await conn.OpenAsync();

            var result = await command.ExecuteScalarAsync();
            
            if (result == null || result == DBNull.Value)
            {
                throw new Exception("Procedura nie zwróciła ID.");
            }
            res = Convert.ToInt32(result);
        }
        return res;
    }
    
    
}