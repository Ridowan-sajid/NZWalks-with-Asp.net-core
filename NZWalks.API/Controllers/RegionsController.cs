using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RegionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        //Create Region
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddRegionDto addRegionDto)
        {
            var region = new Region() {
                Name = addRegionDto.Name,
                Code = addRegionDto.Code,
                RegionImgUrl = addRegionDto.RegionImgUrl,
            };

            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImgUrl = region.RegionImgUrl,
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //Get all region
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _context.Regions.ToListAsync();

            if (regions == null)
            {
                return NotFound();
            }

            var regionsDto = new List<RegionDto>();
            foreach (var region in regions)
            {
                regionsDto.Add(new RegionDto() {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImgUrl = region.RegionImgUrl
                });
            }


            return Ok(regionsDto);
        }

        //Get single region
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var regions = await _context.Regions.Where(e => e.Id == id).FirstOrDefaultAsync();
            if (regions == null)
            {
                return NotFound();
            }

            var regionDto = new RegionDto()
            {
                Id = regions.Id,
                Name = regions.Name,
                Code = regions.Code,
                RegionImgUrl = regions.RegionImgUrl
            };

            return Ok(regionDto);
        }

        //Update Region
        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody] AddRegionDto addRegionDto)
        {
            var region = await _context.Regions.FirstOrDefaultAsync(e=>e.Id==id);

            if (region == null)
            {
                return NotFound("You input wrong id!");
            }

            region.Name = addRegionDto.Name;
            region.Code = addRegionDto.Code;
            region.RegionImgUrl = addRegionDto.RegionImgUrl;
            //_context.Regions.Update(reg);   ==Not required this line because EF core tracking these Three property
            await _context.SaveChangesAsync();

            var regionDto = new RegionDto()
            {
                Id= region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImgUrl = region.RegionImgUrl
            };

            return Ok(regionDto);
        }


        //Delete region
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var regions = await _context.Regions.Where(e => e.Id == id).FirstOrDefaultAsync();
            
            if (regions == null)
            {
                return NotFound();
            }
            _context.Regions.Remove(regions);
            await _context.SaveChangesAsync();

            //var regionDto = new RegionDto()
            //{
            //    Id = regions.Id,
            //    Name = regions.Name,
            //    Code = regions.Code,
            //    RegionImgUrl = regions.RegionImgUrl
            //};

            return Ok("Deleted");
        }
    }
}
