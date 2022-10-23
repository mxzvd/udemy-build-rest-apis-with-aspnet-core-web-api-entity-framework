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

    public IEnumerable<Region> GetAll()
    {
        return dbContext.Regions.ToList();
    }
}
