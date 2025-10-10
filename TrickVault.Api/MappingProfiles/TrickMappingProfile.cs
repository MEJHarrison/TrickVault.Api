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
                .ForMember(destination => destination.Categories, config => config.MapFrom(source => source.TrickCategories.Select(tc => tc.Category)));
            CreateMap<CreateTrickDto, Trick>()
                .ForMember(destination => destination.TrickCategories, config => config.Ignore());
        }
    }
}
