using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Core.Models;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories
{
    public interface IRecipesRepository
    {
        Recipe Save(Recipe recipe);
        
        Task<Recipe> GetFullRecipeAsync(int id);

        Task<Recipe> GetSingleRecipeAsync(int id);

        Task<IList<Recipe>> GetRecipesAsync(IList<int> ids);

        Task<IList<Recipe>> GetRecipesAsync(IList<int> excludedRecipeIds, bool onlyVegetarian, MinutesInterval? totalTimeInterval, IList<string> chefs);

        Task<List<Recipe>> GetAllAsync();

        Task<IList<int>> GetAllIdsAsync();

    }
}
