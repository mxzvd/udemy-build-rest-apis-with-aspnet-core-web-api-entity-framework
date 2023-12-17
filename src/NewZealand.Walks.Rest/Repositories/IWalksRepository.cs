namespace NewZealand.Walks.Rest.Repositories;

public interface IWalksRepository
{
    Task<IEnumerable<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null);
    Task<Walk?> GetWalkByWalkIdAsync(Guid id);
    Task<Walk> AddWalkAsync(Walk entity);
    Task<Walk?> UpdateWalkByWalkIdAsync(Guid id, Walk entity);
    Task<Walk?> DeleteWalkByWalkIdAsync(Guid id);
}
