using AutoMapper;
using TrickVault.Api.DTOs.Category;
using TrickVault.Api.Models;

namespace TrickVault.Api.MappingProfiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, GetCategoriesDto>();
            CreateMap<Category, GetCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
