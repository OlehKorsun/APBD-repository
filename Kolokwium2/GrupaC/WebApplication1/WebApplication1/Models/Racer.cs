using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.DTOs;

namespace WebApplication1.Models;

[Table("Racer")]
public class Racer
{
    [Key]
    public int RacerId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; }
    
    public ICollection<RaceParticipation> RaceParticipations { get; set; }
}