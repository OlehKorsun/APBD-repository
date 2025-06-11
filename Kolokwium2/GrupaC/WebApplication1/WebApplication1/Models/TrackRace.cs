using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Track_Race")]
public class TrackRace
{
    [Key]
    public int TrackRaceId { get; set; }
    
    [Required]
    public int Laps { get; set; }
    public int? BestTimeInSeconds { get; set; }
    
    [ForeignKey(nameof(TrackId))]
    public Track Track { get; set; }
    
    [ForeignKey(nameof(RaceId))]
    public Race Race { get; set; }
    
    public int TrackId { get; set; }
    public int RaceId { get; set; }
    
    public ICollection<RaceParticipation> RaceParticipation { get; set; }
}