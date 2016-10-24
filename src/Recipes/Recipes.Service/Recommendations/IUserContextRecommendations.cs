using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations
{
    /// <summary>
    /// Service working with recommendations based on current user context.
    /// Service takes user history, and current recipe metadata into account
    /// </summary>
    public interface IUserContextRecommendations
    {
        /// <summary>
        /// Returns list of <see cref="RecipeRecommendation"/> based on passed <see cref="ContextBasedFilter"/>
        /// </summary>
        /// <param name="filter">Context filter to be used</param>
        /// <returns>List of appropriate recommendations</returns>
        Task<IList<RecipeRecommendation>> Get(ContextBasedFilter filter);
    }
}
