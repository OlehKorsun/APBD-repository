using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Table("Player_Match")]
[PrimaryKey(nameof(PlayerId), nameof(MatchId))]
public class PlayerMatch
{
    [Required]
    public int MVPs { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(4,2)")]
    public double Rating { get; set; }
    
    [ForeignKey(nameof(PlayerId))]
    public Player Player { get; set; }
    
    [ForeignKey(nameof(MatchId))]
    public Match Match { get; set; }
    
    public int PlayerId { get; set; }
    public int MatchId { get; set; }
}