using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly ApplicationDbContext _context;
        public RegionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> DeleteByIdAsync(Guid id)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null)
            {
                return null;
            }
            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> GetRegionByIdAsync(Guid id)
        {
            var region= await _context.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region != null)
            {
                return region;
            }
            return null;
        }

        public async Task<List<Region>> GetRegionsAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var reg = await _context.Regions.FirstOrDefaultAsync(e => e.Id == id);

            if (reg == null)
            {
                return null;
            }

            reg.Name = region.Name;
            reg.Code = region.Code;
            reg.RegionImgUrl = region.RegionImgUrl;

            await _context.SaveChangesAsync();
            return reg;
        }
    }
}
