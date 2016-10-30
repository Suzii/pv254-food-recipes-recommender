using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;

namespace Recipes.DAL.Repositories
{
    public interface IIngredientUsagesRepository
    {
        Task<IList<int>> GetIngredientUsagesForRecipe(int recipeId);

        /// <summary>
        /// Gets Dice coefficients for each recipe based on given ingredients set.
        /// Returns list of {recipeId, coefficient} ordered by coefficient (descending).
        /// </summary>
        /// <param name="ingredientIds">List of ingredient ids to be checked.</param>
        /// <returns>List of {recipeId, coefficient} ordered by coefficient.</returns>
        Task<List<DiceCoefficientHelper>> GetDiceCoefficients(IList<int> ingredientIds);

        /// <summary>
        /// Gets Dice coefficients for each recipe from recipeIds based on given ingredients set.
        /// Returns list of {recipeId, coefficient} ordered by coefficient (descending).
        /// </summary>
        /// <param name="ingredientIds">List of ingredient ids to be checked.</param>
        /// <param name="recipeIds">List of recipes to be checked.</param>
        /// <returns>List of {recipeId, coefficient} ordered by coefficient.</returns>
        Task<List<DiceCoefficientHelper>> GetDiceCoefficientsOnRandomRecipes
            (IList<int> ingredientIds, IList<int> recipeIds);
    }
}