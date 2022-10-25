using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkRepository : IWalkRepository
{
    private readonly NZWalksDbContext dbContext;

    public WalkRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<Walk> AddAsync(Walk walk)
    {
        walk.Id = new();
        await dbContext.Walks.AddAsync(walk);
        await dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<Walk> DeleteAsync(Guid id)
    {
        var existingWalk = await dbContext.Walks.FindAsync(id);

        if (existingWalk == null)
        {
            return null;
        }

        dbContext.Walks.Remove(existingWalk);
        await dbContext.SaveChangesAsync();
        return existingWalk;
    }

    public async Task<IEnumerable<Walk>> GetAllAsync()
    {
        return await dbContext.Walks
            .Include(e => e.Region)
            .Include(e => e.WalkDifficulty)
            .ToListAsync();
    }

    public async Task<Walk> GetAsync(Guid id)
    {
        return await dbContext.Walks
            .Include(e => e.Region)
            .Include(e => e.WalkDifficulty)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Walk> UpdateAsync(Guid id, Walk walk)
    {
        var existingWalk = await dbContext.Walks.FindAsync(id);

        if (existingWalk == null)
        {
            return null;
        }

        existingWalk.Length = walk.Length;
        existingWalk.Name = walk.Name;
        existingWalk.WalkDifficultyId = walk.WalkDifficultyId;
        existingWalk.RegionId = walk.RegionId;
        await dbContext.SaveChangesAsync();
        return existingWalk;
    }
}
