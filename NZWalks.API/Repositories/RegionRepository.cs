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

    public async Task<IEnumerable<Region>> GetAllAsync()
    {
        return await dbContext.Regions.ToListAsync();
    }
}
