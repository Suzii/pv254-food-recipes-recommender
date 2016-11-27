using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;

namespace Recipes.Service.Recommendations
{
    /// <summary>
    /// Service working with recommendations based on their popularity.
    /// </summary>
    public interface IMostPopularRecommendations
    {
        /// <summary>
        /// Returns list of <see cref="RecipeRecommendation"/> based on recipes popularity, e.g. number of times recipe has been clicked on
        /// </summary>
        /// <param name="count">Number of reecommendations to be return</param>
        /// <returns>List of recommendations of most popular recipes</returns>
        Task<IList<RecipeRecommendation>> Get(int count);
    }
}
