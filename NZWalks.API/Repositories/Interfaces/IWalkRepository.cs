using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalksAsync();
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
