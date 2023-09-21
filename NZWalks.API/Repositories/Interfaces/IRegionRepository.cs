using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetRegionsAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id, Region region);
        Task<Region?> DeleteByIdAsync(Guid id);

    }
}
