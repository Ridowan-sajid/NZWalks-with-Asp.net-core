using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
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
        private readonly IMapper _mapper;

        public RegionsController(IRegionRepository regionRepository,IMapper mapper)
        {
            _regionRepository = regionRepository;
            _mapper = mapper;
        }

        //Create Region
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddRegionDto addRegionDto)
        {
            if(ModelState.IsValid)
            {
                    var region=_mapper.Map<Region>(addRegionDto);

                    region=await _regionRepository.CreateAsync(region);

                    var regionDto=_mapper.Map<RegionDto>(region);

                    return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        
            }
            return BadRequest();
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

            var regionsDto = _mapper.Map<List<RegionDto>>(regions);


            return Ok(regionsDto);
        }

        //Get single region
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await _regionRepository.GetRegionByIdAsync(id);
            if (region == null)
            {
                return NotFound();
            }


            var regionDto= _mapper.Map<RegionDto>(region);

            return Ok(regionDto);
        }

        //Update Region
        [HttpPut("{id:Guid}")]
        //After using ValidateModel we don't need to check one by one with if(ModeState.isValid).
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id,[FromBody] AddRegionDto addRegionDto)
        {
            var region = _mapper.Map<Region>(addRegionDto);
            var regionUpdated=await _regionRepository.UpdateAsync(id, region);
           
            var regionDto=_mapper.Map<RegionDto>(regionUpdated);

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
