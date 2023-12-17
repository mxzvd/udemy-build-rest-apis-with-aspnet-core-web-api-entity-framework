namespace NewZealand.Walks.Rest.Models.DataTransferObjects;

public sealed record GetWalkResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Description { get; init; }
    public required double LengthInKm { get; init; }

    public string? WalkImageUrl { get; init; } = null;

    public required GetRegionResponse Region { get; init; }
    public required GetDifficultyResponse Difficulty { get; init; }
}
