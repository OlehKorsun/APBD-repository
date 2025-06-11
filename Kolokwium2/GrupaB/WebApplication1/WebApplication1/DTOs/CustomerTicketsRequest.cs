using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class CustomerTicketsRequest
{
    [Required]
    public CustomerRequest Customer { get; set; }
    
    [Required]
    public List<PurchasesRequest> Purchases { get; set; }
}

public class CustomerRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    public string? PhoneNumber { get; set; }
}

public class PurchasesRequest
{
    [Required]
    public int SeatNumber { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string ConcertName { get; set; }
    
    [Required]
    public double Price { get; set; }
}