using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
