namespace NewZealand.Walks.Rest.Models.Entities;

public class Walk
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required double LengthInKm { get; set; }
    public string? WalkImageUrl { get; set; }
    public required Guid DifficultyId { get; set; }
    public required Guid RegionId { get; set; }

    public Region RegionNavigation { get; set; } = null!;
    public Difficulty DifficultyNavigation { get; set; } = null!;
}
