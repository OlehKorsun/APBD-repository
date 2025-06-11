using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Purchased_Ticket")]
[PrimaryKey(nameof(CustomerId), nameof(TicketConcertId))]
public class PurchasedTicket
{
    [ForeignKey(nameof(CustomerId))]
    public Customer Customer { get; set; }
    
    [ForeignKey(nameof(TicketConcertId))]
    public TicketConcert TicketConcert { get; set; }
    
    public int CustomerId { get; set; }
    public int TicketConcertId { get; set; }
    
    [Required]
    public DateTime PurchaseDate { get; set; }
}