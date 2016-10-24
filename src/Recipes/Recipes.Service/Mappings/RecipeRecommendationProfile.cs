using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class RecipeRecommendationProfile : Profile
    {
        public RecipeRecommendationProfile()
        {
            CreateMap<DAL.Entities.Recipe, DTOs.RecipeRecommendation>();
        }
    }
}
