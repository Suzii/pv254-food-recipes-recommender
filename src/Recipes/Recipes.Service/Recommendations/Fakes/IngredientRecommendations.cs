using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Fakes
{
    /// <summary>
    /// Fake implementation for <see cref="IIngredientRecommendations"/>
    /// 
    /// See how Castle Dependency injection is configured in Recipes.Web.CastleInstallers.ServiceInstallers
    /// </summary>
    public class IngredientRecommendations : IIngredientRecommendations
    {
        public Task<IList<RecipeRecommendation>> Get(IngredientBasedFilter filter)
        {
            throw new System.NotImplementedException();
        }
    }
}