using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Race_Participation")]
[PrimaryKey(nameof(RacerId), nameof(TrackRaceId))]
public class RaceParticipation
{
    [Required]
    public int FinishTimeInSeconds { get; set; }
    
    [Required]
    public int Position { get; set; }
    
    [ForeignKey(nameof(RacerId))]
    public Racer Racer { get; set; }
    
    [ForeignKey(nameof(TrackRaceId))]
    public TrackRace TrackRace { get; set; }
    
    public int RacerId { get; set; }
    public int TrackRaceId { get; set; }
}