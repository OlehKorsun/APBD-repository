using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Product_Order")]
[PrimaryKey(nameof(ProductId), nameof(OrderId))]
public class Product_Order
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    
    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; }
    
    [Required]
    public int Amount { get; set; }
}