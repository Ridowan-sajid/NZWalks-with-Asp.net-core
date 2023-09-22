using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public WalkRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Walk> CreateAsync(Walk walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
        }



        public async Task<Walk?> GetWalkByIdAsync(Guid id)
        {
            //We used include to see Region and difficulty data which is connected to Walk through RegionId and DifficultyId
            return await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<List<Walk>> GetWalksAsync()
        {
            return await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var res = await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(e => e.Id == id);
            if (res == null)
            {
                return null;
            }
            res.Name = walk.Name;
            res.Description = walk.Description;
            res.LengthInKm = walk.LengthInKm;
            res.WalkImgUrl = walk.WalkImgUrl;
            res.DifficultyId = walk.DifficultyId;
            res.RegionId = walk.RegionId;

            await _dbContext.SaveChangesAsync();
            return res;

        }

        public async Task<Walk?> DeleteByIdAsync(Guid id)
        {
            var res = await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(e => e.Id == id);
            if (res == null)
            {
                return null;
            }
            _dbContext.Walks.Remove(res);
            await _dbContext.SaveChangesAsync();
            return res;
        }
    }
}
