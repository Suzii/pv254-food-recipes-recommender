using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<DAL.Entities.Ingredient, DTOs.Ingredient>();
            CreateMap<DTOs.Ingredient, DAL.Entities.Ingredient>();
        }
    }
}
