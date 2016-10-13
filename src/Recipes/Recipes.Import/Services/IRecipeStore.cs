using Recipes.DAL.Entities;

namespace Recipes.Import.Services
{
    public interface IRecipeStore
    {
        /// <summary>
        /// Saves recipe and returns its id.
        /// </summary>
        /// <param name="recipe">Recipe to be saved</param>
        /// <returns>Id of the recipe</returns>
        int SaveRecipe(Recipe recipe);
    }
}
