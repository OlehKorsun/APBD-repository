using WebApplication1.Models;

namespace WebApplication1.DTOs;

public class CustomerDTO
{
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string? PhoneNumber { get; set; }
    public List<PurchasesDTO> Purchases { get; set; }
}

public class PurchasesDTO
{
    public DateTime Date { get; set; }
    public double Price { get; set; }
    public TicketDTO Ticket { get; set; } 
    public ConcertDTO Concert { get; set; }
}

public class TicketDTO
{
    public string Serial { get; set; }
    public int SeatNumber { get; set; }
}

public class ConcertDTO
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
}