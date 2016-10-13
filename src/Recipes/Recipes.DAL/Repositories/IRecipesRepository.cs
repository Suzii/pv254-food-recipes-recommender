using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories
{
    public interface IRecipesRepository
    {
        Recipe Save(Recipe recipe);

        Recipe GetFullRecipe(int id);

        Task<Recipe> GetFullRecipeAsync(int id);

        Task<List<Recipe>> GetAllAsync();
    }
}
