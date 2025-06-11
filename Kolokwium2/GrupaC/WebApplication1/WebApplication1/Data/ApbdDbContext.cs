using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApbdDbContext : DbContext
{
    
    public DbSet<Race> Races { get; set; }
    public DbSet<TrackRace> TrackRaces { get; set; }
    public DbSet<Racer> Racers { get; set; }
    public DbSet<Track> Tracks { get; set; }
    public DbSet<RaceParticipation> RaceParticipations { get; set; }
    
    
    protected ApbdDbContext()
    {
    }

    public ApbdDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Racer>().HasData(new List<Racer>()
        {
            new Racer(){RacerId = 1, FirstName = "John", LastName = "Doe"},
            new Racer(){RacerId = 2, FirstName = "Jane", LastName = "Doe"},
            new Racer(){RacerId = 3, FirstName = "Jinx", LastName = "Doe"},
        });

        modelBuilder.Entity<Race>().HasData(new List<Race>()
        {
            new Race(){RaceId = 1, Name = "Race 1", Location = "Location 1", Date = DateTime.Parse("2024-05-05")},
            new Race(){RaceId = 2, Name = "Race 2", Location = "Location 2", Date = DateTime.Parse("2024-05-06")},
            new Race(){RaceId = 3, Name = "Race 3", Location = "Location 3", Date = DateTime.Parse("2024-05-09")},
        });

        modelBuilder.Entity<Track>().HasData(new List<Track>()
        {
            new Track(){TrackId = 1, Name = "Track 1", LengthInKm = 55},
            new Track(){TrackId = 2, Name = "Track 2", LengthInKm = 24},
            new Track(){TrackId = 3, Name = "Track 3", LengthInKm = 178},
        });

        modelBuilder.Entity<TrackRace>().HasData(new List<TrackRace>()
        {
            new TrackRace(){RaceId = 1, TrackId = 1, TrackRaceId = 1, Laps = 5, BestTimeInSeconds = 1565},
            new TrackRace(){RaceId = 2, TrackId = 2, TrackRaceId = 2, Laps = 7, BestTimeInSeconds = 91565},
            new TrackRace(){RaceId = 3, TrackId = 3, TrackRaceId = 3, Laps = 1, BestTimeInSeconds = 166},
        });

        modelBuilder.Entity<RaceParticipation>().HasData(new List<RaceParticipation>()
        {
            new RaceParticipation(){RacerId = 1, TrackRaceId = 1, Position = 1, FinishTimeInSeconds = 678},
            new RaceParticipation(){RacerId = 2, TrackRaceId = 2, Position = 2, FinishTimeInSeconds = 567},
            new RaceParticipation(){RacerId = 3, TrackRaceId = 3, Position = 3, FinishTimeInSeconds = 1734},
        });
    }
}