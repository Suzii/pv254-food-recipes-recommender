using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations
{
    /// <summary>
    /// Service working with recommendations based on user-selected ingredients and total (preparation + cooking) time
    /// </summary>
    public interface IIngredientRecommendations
    {
        /// <summary>
        /// Returns list of <see cref="RecipeRecommendation"/> based on passed <see cref="IngredientBasedFilter"/>
        /// </summary>
        /// <param name="filter">Ingredient filter to be used</param>
        /// <returns>List of appropriate recommendations</returns>
        Task<IList<RecipeRecommendation>> Get(IngredientBasedFilter filter);
    }
}
