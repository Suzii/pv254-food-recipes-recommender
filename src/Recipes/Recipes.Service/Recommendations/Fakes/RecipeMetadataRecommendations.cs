using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Fakes
{
    /// <summary>
    /// Fake implementation for <see cref="IRecipeMetadataRecommendations"/>
    /// 
    /// See how Castle Dependency injection is configured in Recipes.Web.CastleInstallers.ServiceInstallers
    /// </summary>
    public class RecipeMetadataRecommendations : IRecipeMetadataRecommendations
    {
        public Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            throw new System.NotImplementedException();
        }
    }
}