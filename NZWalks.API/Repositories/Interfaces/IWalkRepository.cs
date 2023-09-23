using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetWalksAsync(string? filterOn=null,string? filterQuery=null,string? sorting=null,bool? isAssending=true, int pageNumber=1, int pageSize=100);
        Task<Walk?> GetWalkByIdAsync(Guid id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(Guid id, Walk walk);
        Task<Walk?> DeleteByIdAsync(Guid id);
    }
}
