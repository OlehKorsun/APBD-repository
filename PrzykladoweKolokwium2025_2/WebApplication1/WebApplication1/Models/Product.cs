using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Product")]
[Index(nameof(Id))]
public class Product
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [Column(TypeName = "numeric")]
    [Precision(10, 2)]
    public double Price { get; set; }
    
    public ICollection<Product_Order> ProductOrders { get; set; }
}