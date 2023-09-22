using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IWalkRepository _walkRepository;
        private readonly IMapper _mapper;
        public WalksController(IWalkRepository walkRepository,IMapper mapper)
        {
            _walkRepository = walkRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] AddWalkDto addWalkDto)
        {
            var walk = _mapper.Map<Walk>(addWalkDto);

            walk = await _walkRepository.CreateAsync(walk);

            var walkDto = _mapper.Map<WalkDto>(walk);

            return Ok(walkDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWalk()
        {
            var res=await _walkRepository.GetWalksAsync();
            if (res == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<List<WalkDto>>(res));
            
        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetWalkById(Guid id)
        {
            var res = await _walkRepository.GetWalkByIdAsync(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(res));

        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateWalk(Guid id,AddWalkDto addWalkDto)
        {
            var Walk=_mapper.Map<Walk>(addWalkDto);
            var res = await _walkRepository.UpdateAsync(id,Walk);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(res));

        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteWalk(Guid id)
        {
            var res = await _walkRepository.DeleteByIdAsync(id);
            if (res == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<WalkDto>(res));

        }

    }
}
