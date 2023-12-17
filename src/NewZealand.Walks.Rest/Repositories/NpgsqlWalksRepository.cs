using Microsoft.EntityFrameworkCore;

namespace NewZealand.Walks.Rest.Repositories;

public class NpgsqlWalksRepository : IWalksRepository
{
    private readonly NewZealandWalksDbContext dbContext;

    public NpgsqlWalksRepository(NewZealandWalksDbContext dbContext) => this.dbContext = dbContext;

    public async Task<IEnumerable<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null)
    {
        var query = dbContext.Walks
            .Include(e => e.DifficultyNavigation)
            .Include(e => e.RegionNavigation)
            .AsQueryable();

        // Apply filtering
        if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
        {
            if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                query = query.Where(e => e.Name.Contains(filterQuery));
        }

        return await query.ToListAsync();
    }

    public async Task<Walk?> GetWalkByWalkIdAsync(Guid id)
    {
        return await dbContext.Walks
            .Include(e => e.DifficultyNavigation)
            .Include(e => e.RegionNavigation)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Walk> AddWalkAsync(Walk entity)
    {
        dbContext.Walks.Add(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Walk?> UpdateWalkByWalkIdAsync(Guid id, Walk entity)
    {
        var existingEntity = await dbContext.Walks
            .Include(e => e.DifficultyNavigation)
            .Include(e => e.RegionNavigation)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existingEntity == null)
            return null;

        existingEntity.Name = entity.Name;
        existingEntity.Description = entity.Description;
        existingEntity.LengthInKm = entity.LengthInKm;
        existingEntity.WalkImageUrl = entity.WalkImageUrl;
        existingEntity.DifficultyId = entity.DifficultyId;
        existingEntity.RegionId = entity.RegionId;

        await dbContext.SaveChangesAsync();
        return existingEntity;
    }

    public async Task<Walk?> DeleteWalkByWalkIdAsync(Guid id)
    {
        var existingEntity = await dbContext.Walks
            .Include(e => e.DifficultyNavigation)
            .Include(e => e.RegionNavigation)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (existingEntity == null)
            return null;

        dbContext.Walks.Remove(existingEntity);
        await dbContext.SaveChangesAsync();
        return existingEntity;
    }
}
