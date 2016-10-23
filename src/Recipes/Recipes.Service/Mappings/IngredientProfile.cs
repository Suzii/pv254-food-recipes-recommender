using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<DAL.Entities.Ingredient, DTOs.Ingredient>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<DTOs.Ingredient, DAL.Entities.Ingredient>()
                .ForMember(dest => dest.IngredientUsages, opt => opt.Ignore());
        }
    }
}
