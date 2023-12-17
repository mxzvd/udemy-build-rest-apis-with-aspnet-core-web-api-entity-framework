namespace NewZealand.Walks.Rest.Repositories;

public interface IRegionsRepository
{
    Task<IEnumerable<Region>> GetRegionsAsync();
    Task<Region?> GetRegionByRegionIdAsync(Guid id);
    Task<Region> AddRegionAsync(Region entity);
    Task<Region?> UpdateRegionByRegionIdAsync(Guid id, Region entity);
    Task<Region?> DeleteRegionByRegionIdAsync(Guid id);
}
