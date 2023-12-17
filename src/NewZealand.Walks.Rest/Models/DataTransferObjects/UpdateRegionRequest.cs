using System.ComponentModel.DataAnnotations;

namespace NewZealand.Walks.Rest.Models.DataTransferObjects;

public sealed record UpdateRegionRequest
{
    [Required]
    [MinLength(3, ErrorMessage = "Code must be of minimum 3 characters.")]
    [MaxLength(3, ErrorMessage = "Code must be of maximum 3 characters.")]
    public required string Code { get; init; }

    [Required]
    [MaxLength(100, ErrorMessage = "Code must be of maximum 100 characters.")]
    public required string Name { get; init; }

    public string? RegionImageUrl { get; init; } = null;
}
