namespace NewZealand.Walks.Rest.Models.DataTransferObjects;

public sealed record GetRegionResponse
{
    public required Guid Id { get; init; }
    public required string Code { get; init; }
    public required string Name { get; init; }
    public string? RegionImageUrl { get; init; } = null;
}
