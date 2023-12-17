namespace NewZealand.Walks.Rest.Models.DataTransferObjects;

public sealed record GetDifficultyResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}
