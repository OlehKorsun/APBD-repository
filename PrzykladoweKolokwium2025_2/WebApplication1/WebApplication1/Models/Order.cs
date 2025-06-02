using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Order")]
[Index(nameof(Id))]
public class Order
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public DateTime CreatedAt { get; set; }
    
    public DateTime? FulfilledAt { get; set; }
    
    public int ClientId { get; set; }
    public int StatusId { get; set; }
    
    [ForeignKey(nameof(ClientId))]
    public Client Client { get; set; }
    
    [ForeignKey(nameof(StatusId))]
    public Status Status { get; set; }
    
    public ICollection<Product_Order> ProductOrders { get; set; }
}