using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Table("Match")]
public class Match
{
    [Key]
    public int MatchId { get; set; }
    
    [Required]
    public DateTime MatchDate { get; set; }
    
    [Required]
    public int Team1Score { get; set; }
    
    [Required]
    public int Team2Score { get; set; }
    
    [Column(TypeName = "decimal(4, 2)")]
    public double? BestRating { get; set; }
    
    [ForeignKey(nameof(MapId))]
    public Map Map { get; set; }
    
    [ForeignKey(nameof(TournamentId))]
    public Tournament Tournament { get; set; }
    
    public int MapId { get; set; }
    public int TournamentId { get; set; }
    
    public ICollection<PlayerMatch> PlayerMatches { get; set; }
}