using Microsoft.EntityFrameworkCore;

namespace NewZealand.Walks.Rest.Data;

public class NewZealandWalksDbContext : DbContext
{
    private readonly IConfiguration configuration;

    public DbSet<Difficulty> Difficulties { get; set; } = null!;
    public DbSet<Region> Regions { get; set; } = null!;
    public DbSet<Walk> Walks { get; set; } = null!;

    public NewZealandWalksDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        this.configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data for difficulties
        var defaultDifficulties = new List<Difficulty>
        {
            new()
            {
                Id = new("1780440c-1964-4ddf-8e2a-73a5742d3472"),
                Name = "Easy",
            },
            new()
            {
                Id = new("05c97adb-9920-4254-bfe2-dbbd25da2d5a"),
                Name = "Medium",
            },
            new()
            {
                Id = new("7fd06a9a-50ab-45b0-9f66-e81231812cb5"),
                Name = "Hard",
            },
        };

        modelBuilder.Entity<Difficulty>().HasData(defaultDifficulties);

        // Seed data for regions
        var defaultRegions = new List<Region>
        {
            new Region
            {
                Id = new("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                Name = "Auckland",
                Code = "AKL",
                RegionImageUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = new("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
                Name = "Northland",
                Code = "NTL",
                RegionImageUrl = null
            },
            new Region
            {
                Id = new("14ceba71-4b51-4777-9b17-46602cf66153"),
                Name = "Bay Of Plenty",
                Code = "BOP",
                RegionImageUrl = null
            },
            new Region
            {
                Id = new("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                Name = "Wellington",
                Code = "WGN",
                RegionImageUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = new("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                Name = "Nelson",
                Code = "NSN",
                RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            },
            new Region
            {
                Id = new("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                Name = "Southland",
                Code = "STL",
                RegionImageUrl = null
            },
        };

        modelBuilder.Entity<Region>().HasData(defaultRegions);
    }
}
