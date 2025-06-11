using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Ticket")]
public class Ticket
{
    [Key]
    public int TicketId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string SerialNumber { get; set; }
    
    [Required]
    public int SeatNumber { get; set; }
    
    public ICollection<TicketConcert> TicketConcerts { get; set; }
}