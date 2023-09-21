using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionRepository _regionRepository;
        public RegionsController(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
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

            region=await _regionRepository.CreateAsync(region);

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
            var regions = await _regionRepository.GetRegionsAsync();

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
            var regions = await _regionRepository.GetRegionByIdAsync(id);
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
            var region = new Region()
            {
                Name = addRegionDto.Name,
                Code = addRegionDto.Code,
                RegionImgUrl = addRegionDto.RegionImgUrl
            };

            var regionModel=await _regionRepository.UpdateAsync(id, region);


            var regionDto = new RegionDto()
            {
                Id= regionModel.Id,
                Name = regionModel.Name,
                Code = regionModel.Code,
                RegionImgUrl = regionModel.RegionImgUrl
            };

            return Ok(regionDto);
        }


        //Delete region
        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var region = await _regionRepository.DeleteByIdAsync(id);
            if(region == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }
    }
}
