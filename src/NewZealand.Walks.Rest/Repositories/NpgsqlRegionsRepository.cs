using Microsoft.EntityFrameworkCore;

namespace NewZealand.Walks.Rest.Repositories;

public class NpgsqlRegionsRepository : IRegionsRepository
{
    private readonly NewZealandWalksDbContext dbContext;

    public NpgsqlRegionsRepository(NewZealandWalksDbContext dbContext) => this.dbContext = dbContext;

    public async Task<IEnumerable<Region>> GetRegionsAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetRegionByRegionIdAsync(Guid id)
    {
        return await dbContext.Regions.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Region> AddRegionAsync(Region entity)
    {
        dbContext.Regions.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Region?> UpdateRegionByRegionIdAsync(Guid id, Region entity)
    {
        var existingEntity = await dbContext.Regions.FirstOrDefaultAsync(e => e.Id == id);

        if (existingEntity == null)
            return null;

        existingEntity.Code = entity.Code;
        existingEntity.Name = entity.Name;
        existingEntity.RegionImageUrl = entity.RegionImageUrl;

        await dbContext.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<Region?> DeleteRegionByRegionIdAsync(Guid id)
    {
        var existingEntity = await dbContext.Regions.FirstOrDefaultAsync(e => e.Id == id);

        if (existingEntity == null)
            return null;

        dbContext.Regions.Remove(existingEntity);
        await dbContext.SaveChangesAsync();
        return existingEntity;
    }
}
