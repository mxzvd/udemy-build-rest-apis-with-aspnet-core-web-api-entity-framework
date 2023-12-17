namespace NewZealand.Walks.Rest.Models.Entities;

public class Region
{
    public Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}
