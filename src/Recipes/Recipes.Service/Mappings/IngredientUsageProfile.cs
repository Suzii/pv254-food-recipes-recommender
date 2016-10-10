using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class IngredientUsageProfile : Profile
    {
        public IngredientUsageProfile()
        {
            CreateMap<DAL.Entities.IngredientUsage, DTOs.IngredientUsage>();
            CreateMap<DTOs.IngredientUsage, DAL.Entities.IngredientUsage>();
        }
    }
}
