using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories.Implementation
{
    public class RecipesRepository : IRecipesRepository
    {
        public Recipe GetFullRecipe(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Include(r => r.IngredientUsages)
                    .Single(r => r.Id == id);

                return result;
            }
        }

        public Task<Recipe> GetFullRecipeAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Include(r => r.IngredientUsages)
                    .SingleAsync(r => r.Id == id);

                return result;
            }
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Include(r => r.IngredientUsages)
                    .ToListAsync();

                return await result;
            }
        }
    }
}
