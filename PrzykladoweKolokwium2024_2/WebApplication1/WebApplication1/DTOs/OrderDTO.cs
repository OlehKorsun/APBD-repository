namespace WebApplication1.DTOs;

public class OrderDTO
{
    public int Id { get; set; }
    public DateTime AcceptedAt { get; set; }
    public DateTime? FulfilledAt { get; set; }
    public string? Comments { get; set; }
    public List<PastryDTO> Pastries { get; set; }
}

public class PastryDTO
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }
}