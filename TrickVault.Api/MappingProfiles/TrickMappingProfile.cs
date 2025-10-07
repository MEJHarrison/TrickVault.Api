using AutoMapper;
using TrickVault.Api.DTOs.Trick;
using TrickVault.Api.Models;

namespace TrickVault.Api.MappingProfiles
{
    public class TrickMappingProfile : Profile
    {
        public TrickMappingProfile()
        {
            CreateMap<Trick, GetTricksDto>();
            CreateMap<Trick, GetTrickDto>()
                .ForMember(destination => destination.Categories, config => config.MapFrom(source => source.Categories));
            CreateMap<CreateTrickDto, Trick>()
                .ForMember(destination => destination.Categories, config => config.Ignore());
        }
    }
}
