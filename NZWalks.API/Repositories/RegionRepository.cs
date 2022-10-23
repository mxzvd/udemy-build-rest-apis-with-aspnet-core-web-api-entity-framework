using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class RegionRepository : IRegionRepository
{
    private readonly NZWalksDbContext dbContext;

    public RegionRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Region> AddAsync(Region region)
    {
        region.Id = new();
        await dbContext.AddAsync(region);
        await dbContext.SaveChangesAsync();
        return region;
    }

    public async Task<Region> DeleteAsync(Guid id)
    {
        var region = await dbContext.Regions.FirstOrDefaultAsync(e => e.Id == id);

        if (region == null)
        {
            return null;
        }

        dbContext.Regions.Remove(region);
        await dbContext.SaveChangesAsync();

        return region;
    }

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region> GetAsync(Guid id)
    {
        return await dbContext.Regions.FirstOrDefaultAsync(e => e.Id.Equals(id));
    }

    public async Task<Region> UpdateAsync(Guid id, Region region)
    {
        var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(e => e.Id == id);

        if (existingRegion == null)
        {
            return null;
        }

        existingRegion.Code = region.Code;
        existingRegion.Name = region.Name;
        existingRegion.Area = region.Area;
        existingRegion.Lat = region.Lat;
        existingRegion.Long = region.Long;
        existingRegion.Population = region.Population;

        await dbContext.SaveChangesAsync();

        return existingRegion;
    }
}
