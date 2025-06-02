namespace WebApplication1.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Status { get; set; }
    public ClientDTO Client { get; set; }
    public List<ProductDTO> Products { get; set; }
}


public class ClientDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}


public class ProductDTO
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
}