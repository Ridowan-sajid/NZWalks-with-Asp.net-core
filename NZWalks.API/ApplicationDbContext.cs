using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API
{
    //public class ApplicationDbContext:DbContext
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) { }

        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //var difficulties = new List<Difficulty>()
            //{
            //    new Difficulty()
            //    {
            //        Id=Guid.Parse("e71ce98e-bed5-4d99-a79e-8d6f1250b5ec"),
            //        Name="Easy"
            //    },
            //    new Difficulty()
            //    {
            //        Id=Guid.Parse("972a14b6-f687-4c2c-b1e2-f9f72dc64965"),
            //        Name="Medium"
            //    },
            //    new Difficulty()
            //    {
            //        Id=Guid.Parse("bf2e778e-4ef5-4d1c-a96e-49aff2fb902e"),
            //        Name="Hard"
            //    }
            //};

            //modelBuilder.Entity<Difficulty>().HasData(difficulties);


            //// Seed data for Regions
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id = Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
            //        Name = "Auckland",
            //        Code = "AKL",
            //        RegionImgUrl = "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    },
            //    new Region
            //    {
            //        Id = Guid.Parse("6884f7d7-ad1f-4101-8df3-7a6fa7387d81"),
            //        Name = "Northland",
            //        Code = "NTL",
            //        RegionImgUrl = null
            //    },
            //    new Region
            //    {
            //        Id = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
            //        Name = "Bay Of Plenty",
            //        Code = "BOP",
            //        RegionImgUrl = null
            //    },
            //    new Region
            //    {
            //        Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
            //        Name = "Wellington",
            //        Code = "WGN",
            //        RegionImgUrl = "https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    },
            //    new Region
            //    {
            //        Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
            //        Name = "Nelson",
            //        Code = "NSN",
            //        RegionImgUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
            //    },
            //    new Region
            //    {
            //        Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e2502 63"),
            //        Name = "Southland",
            //        Code = "STL",
            //        RegionImgUrl = null
            //    },
            //};

            //modelBuilder.Entity<Region>().HasData(regions);


            var readerRoleId = "4429e51e-67ba-4e02-83d7-ac6a655a5524";
            var writerRoleId = "5c45517a-0850-4bc3-b232-8a0e1e30f4a0";
                            

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper()
                }
            };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

        }
    }
}
