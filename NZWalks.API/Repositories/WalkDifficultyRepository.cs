using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories;

public class WalkDifficultyRepository : IWalkDifficultyRepository
{
    private readonly NZWalksDbContext dbContext;

    public WalkDifficultyRepository(NZWalksDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<WalkDifficulty> AddAsync(WalkDifficulty entity)
    {
        entity.Id = new();
        await dbContext.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<WalkDifficulty> DeleteAsync(Guid id)
    {
        var existingWalkDifficulty = await dbContext.WalkDifficulties.FindAsync(id);

        if (existingWalkDifficulty != null)
        {
            dbContext.WalkDifficulties.Remove(existingWalkDifficulty);
            await dbContext.SaveChangesAsync();
            return existingWalkDifficulty;
        }
        return null;
    }

    public async Task<IEnumerable<WalkDifficulty>> GetAllAsync()
    {
        return await dbContext.WalkDifficulties.ToListAsync();
    }

    public async Task<WalkDifficulty> GetAsync(Guid id)
    {
        return await dbContext.WalkDifficulties.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<WalkDifficulty> UpdateAsync(Guid id, WalkDifficulty entity)
    {
        var existingWalkDifficulty = await dbContext.WalkDifficulties.FindAsync(id);

        if (existingWalkDifficulty == null)
        {
            return null;
        }

        existingWalkDifficulty.Code = entity.Code;
        await dbContext.SaveChangesAsync();

        return existingWalkDifficulty;
    }
}
