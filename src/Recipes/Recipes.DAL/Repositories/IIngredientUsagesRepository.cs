using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;

namespace Recipes.DAL.Repositories
{
    public interface IIngredientUsagesRepository
    {
        /// <summary>
        /// Returns ids of used <see cref="Ingredient"/>s in <see cref="Recipe"/> with <param name="recipeId"></param>
        /// </summary>
        /// <param name="recipeId">Id of recipe whose ingredients should be returned</param>
        /// <returns>Ids of ingredients that recipe contains</returns>
        Task<IList<int>> GetUsedIngredientIds(int recipeId);
        
        /// <summary>
        /// Gets Dice coefficients for each recipe from recipeIdsToBeConsidered based on given ingredients set.
        /// If no <param name="recipeIdsToBeConsidered"></param> are provided, chooses from all available recipes.
        /// Returns list of {recipeId, coefficient} ordered by coefficient (descending).
        /// </summary>
        /// <param name="ingredientIds">List of ingredient ids to be checked.</param>
        /// <param name="recipeIdsToBeConsidered">List of recipes to be checked.</param>
        /// <returns>List of {recipeId, coefficient} ordered by coefficient.</returns>
        Task<List<CoefficientHelper>> GetDiceCoefficients
            (IList<int> ingredientIds, IList<int> recipeIdsToBeConsidered = null);

        /// <summary>
        /// Gets Jaccard coefficients for each recipe from recipeIdsToBeConsidered based on given ingredients set.
        /// If no <param name="recipeIdsToBeConsidered"></param> are provided, chooses from all available recipes.
        /// Returns list of {recipeId, coefficient} ordered by coefficient (descending).
        /// </summary>
        /// <param name="ingredientIds">List of ingredient ids to be checked.</param>
        /// <param name="recipeIdsToBeConsidered">List of recipes to be checked.</param>
        /// <returns>List of {recipeId, coefficient} ordered by coefficient.</returns>
        Task<List<CoefficientHelper>> GetJaccardCoefficients
            (IList<int> ingredientIds, IList<int> recipeIdsToBeConsidered = null);
    }
}