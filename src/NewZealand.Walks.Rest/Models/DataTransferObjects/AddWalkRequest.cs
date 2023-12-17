using System.ComponentModel.DataAnnotations;

namespace NewZealand.Walks.Rest.Models.DataTransferObjects;

public sealed record AddWalkRequest
{
    [Required]
    [MaxLength(100, ErrorMessage = "Name must be of maximum 100 characters.")]
    public required string Name { get; init; }

    [Required]
    [MaxLength(1000, ErrorMessage = "Description must be of maximum 1000 characters.")]
    public required string Description { get; init; }

    [Required]
    [Range(0, 50)]
    public required double LengthInKm { get; init; }

    [Required]
    public required Guid DifficultyId { get; init; }

    [Required]
    public required Guid RegionId { get; init; }

    public string? WalkImageUrl { get; init; } = null;
}
