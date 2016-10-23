using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class IngredientUsageProfile : Profile
    {
        public IngredientUsageProfile()
        {
            CreateMap<DAL.Entities.IngredientUsage, DTOs.IngredientUsage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory))
                .ForMember(dest => dest.FreeText, opt => opt.MapFrom(src => src.FreeText));

            CreateMap<DTOs.IngredientUsage, DAL.Entities.IngredientUsage>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Ingredient, opt => opt.MapFrom(src => src.Ingredient))
                .ForMember(dest => dest.SubCategory, opt => opt.MapFrom(src => src.SubCategory))
                .ForMember(dest => dest.FreeText, opt => opt.MapFrom(src => src.FreeText))
                .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.Ingredient.Id))
                .ForMember(dest => dest.Recipe, opt => opt.Ignore())
                .ForMember(dest => dest.RecipeId, opt => opt.Ignore());
        }
    }
}
