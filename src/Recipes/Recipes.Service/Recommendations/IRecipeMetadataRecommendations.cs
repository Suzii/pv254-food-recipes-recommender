using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations
{
    /// <summary>
    /// Service working with recommendations based solely on recipe metadata
    /// </summary>
    public interface IRecipeMetadataRecommendations
    {
        /// <summary>
        /// Returns list of <see cref="RecipeRecommendation"/> based on passed <see cref="RecipeMetadataBasedFilter"/>
        /// </summary>
        /// <param name="filter">Recipe metadata filter to be used</param>
        /// <returns>List of appropriate recommendations</returns>
        Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter);
    }
}
