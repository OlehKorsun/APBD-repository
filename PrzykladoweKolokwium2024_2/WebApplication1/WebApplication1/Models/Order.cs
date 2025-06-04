using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Order")]
public class Order
{
    [Key]
    public int ID { get; set; }
    
    [Required]
    public DateTime AcceptedAt { get; set; }
    
    public DateTime? FulfilledAt { get; set; }
    
    [MaxLength(300)]
    public string? Comments { get; set; }
    
    public int ClientID { get; set; }
    public int EmployeeID { get; set; }
    
    [ForeignKey(nameof(ClientID))]
    public Client Client { get; set; }
    
    [ForeignKey(nameof(EmployeeID))]
    public Employee Employee { get; set; }
    
    public ICollection<Order_Pastry> PastryOrders { get; set; }
}