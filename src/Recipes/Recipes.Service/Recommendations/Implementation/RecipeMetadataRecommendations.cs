using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Implementation
{
    public class RecipeMetadataRecommendations : IRecipeMetadataRecommendations
    {
        public Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            throw new System.NotImplementedException();
        }
    }
}