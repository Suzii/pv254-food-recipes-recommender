using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class RecipeRecommendationProfile : Profile
    {
        private static readonly char Separator = '#';

        public RecipeRecommendationProfile()
        {
            CreateMap<DAL.Constants.RecommenderType, Constants.RecommenderType>();
            CreateMap<Constants.RecommenderType, DAL.Constants.RecommenderType>();

            CreateMap<DAL.Entities.Recipe, DTOs.RecipeRecommendation>()
                .ForMember(s => s.RecommenderType, config => config.Ignore());

            CreateMap<DTOs.UserActivity.RecommendationUsed, DAL.Entities.RecommendationUsed>()
                .ForMember(s => s.DisplayedRecipe, config => config.Ignore())
                .ForMember(s => s.ClickedRecipe, config => config.Ignore())
                .ForMember(dest => dest.RecommendedBy, opt => opt.ResolveUsing((source, destination) => string.Join(Separator.ToString(), source.RecommendedBy)));
             
            CreateMap<DAL.Entities.RecommendationUsed, DTOs.UserActivity.RecommendationUsed>()
                .ForMember(s => s.RecommendedBy, config => config.ResolveUsing((source, destination) => source.RecommendedBy.Split(Separator)));
        }
    }
}
