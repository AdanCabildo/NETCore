using AutoMapper;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.MappingProfiles
{
    public class FilipinoMappings : Profile
    {
        public FilipinoMappings()
        {
            CreateMap<FilipinoEntity, FilipinoDto>().ReverseMap();
            CreateMap<FilipinoEntity, FilipinoUpdateDto>().ReverseMap();
            CreateMap<FilipinoEntity, FilipinoCreateDto>().ReverseMap();
        }
    }
}