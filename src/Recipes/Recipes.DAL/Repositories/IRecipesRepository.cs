using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories
{
    public interface IRecipesRepository
    {
        Recipe Save(Recipe recipe);
        
        Task<Recipe> GetFullRecipeAsync(int id);

        Task<IList<Recipe>> GetRecipesAsync(IList<int> ids);
            
        Task<List<Recipe>> GetAllAsync();

        Task<IList<int>> GetAllIdsAsync();

        Task<Recipe> GetSingleRecipeAsync(int id);
    }
}
