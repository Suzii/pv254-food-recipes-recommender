using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Implementation
{
    public class IngredientRecommendations : IIngredientRecommendations
    {
        public Task<IList<RecipeRecommendation>> Get(IngredientBasedFilter filter)
        {
            throw new System.NotImplementedException();
        }
    }
}