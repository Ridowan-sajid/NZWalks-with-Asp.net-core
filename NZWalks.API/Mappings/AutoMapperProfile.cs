using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            //Source=RegionDto, Destination=Region
            //With the help of ReverseMap(), we can convert vise versa
            CreateMap<RegionDto, Region>().ReverseMap();
            CreateMap<AddRegionDto, Region>().ReverseMap();
            CreateMap<AddWalkDto, Walk>().ReverseMap();
            CreateMap<WalkDto, Walk>().ReverseMap();
            CreateMap<DifficultyDto, Difficulty>().ReverseMap();
            
        }
    }
}
