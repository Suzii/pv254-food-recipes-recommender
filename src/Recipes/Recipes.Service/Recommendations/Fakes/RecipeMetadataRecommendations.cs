using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Fakes
{
    /// <summary>
    /// Fake implementation for <see cref="IRecipeMetadataRecommendations"/>
    /// 
    /// See how Castle Dependency injection is configured in Recipes.Web.CastleInstallers.ServiceInstallers
    /// </summary>
    public class RecipeMetadataRecommendations : BaseRecommendations, IRecipeMetadataRecommendations
    {
        public RecipeMetadataRecommendations(IRecipesRepository recipesRepository, IMapper mapper) : base(recipesRepository, mapper)
        {
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            var recipeRecommendations = await base.Get(filter);
            foreach (var recipeRecommendation in recipeRecommendations)
            {
                recipeRecommendation.RecommenderType = RecommenderType.RecipeMetadata;
            }

            return recipeRecommendations;
        }
    }
}