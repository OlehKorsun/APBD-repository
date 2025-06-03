using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Pastry")]
public class Pastry
{
    [Key]
    public int ID { get; set; }
    
    [Required]
    [MaxLength(150)]
    public string Name { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(10, 2)")]
    public double Price { get; set; }
    
    [Required]
    [MaxLength(40)]
    public string Type { get; set; }
    
    public ICollection<Order_Pastry> PastryOrders { get; set; }
}