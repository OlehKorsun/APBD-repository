using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs;

public class RacerParticipationRequest
{
    [Required]
    [MaxLength(50)]
    public string RaceName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string TrackName { get; set; }
    
    [Required]
    public List<ParticipationRequest> Participations { get; set; }
}

public class ParticipationRequest
{
    [Required]
    public int RacerId { get; set; }
    
    [Required]
    public int Position { get; set; }
    
    [Required]
    public int FinishTimeInSeconds { get; set; }
}