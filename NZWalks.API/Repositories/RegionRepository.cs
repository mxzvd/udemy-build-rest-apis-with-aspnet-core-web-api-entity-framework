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

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }

    public async Task<Region> GetAsync(Guid id)
    {
        return await dbContext.Regions.FirstAsync(e => e.Id.Equals(id));
    }
}
