using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace WebApplication1.Models;

[Table("Race")]
public class Race
{
    [Key]
    public int RaceId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Location { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    public ICollection<TrackRace> TrackRaces { get; set; }
}