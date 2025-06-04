namespace WebApplication1.DTOs;

public class OrderRequest
{
    public int EmployeeId { get; set; }
    public DateTime AcceptedAt { get; set; }
    public string? Comments { get; set; }
    public List<PastryRequest> Pastries { get; set; } 
}

public class PastryRequest
{
    public string Name { get; set; }
    public int Amount { get; set; }
    public string? Comments { get; set; }
}