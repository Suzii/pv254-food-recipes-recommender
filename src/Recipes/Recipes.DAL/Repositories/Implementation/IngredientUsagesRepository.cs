using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using BugHunter.Core.Extensions;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;

namespace Recipes.DAL.Repositories.Implementation
{
    public class IngredientUsagesRepository : IIngredientUsagesRepository
    {

        public async Task<List<DiceCoefficientHelper>> GetDiceCoefficients(IList<int> ingredientIds, IList<int> recipeIdsToBeConsidered = null)
        {
            using (var dbContext = new AppContext())
            {
                var ingredientUsages = (IQueryable<IngredientUsage>) dbContext.IngredientUsages;
                if (!recipeIdsToBeConsidered.IsNullOrEmpty())
                {
                    ingredientUsages = ingredientUsages.Where(r => recipeIdsToBeConsidered.Contains(r.RecipeId));
                }

                var result = ingredientUsages.GroupBy(ui => ui.RecipeId)
                    .Select(
                        g => new DiceCoefficientHelper
                        {
                            RecipeId = g.Key,
                            Coefficient = 2*g.Count(r => ingredientIds.Contains(r.IngredientId))
                                          /(double) (g.Count() + ingredientIds.Count)
                        })
                    .OrderByDescending(r => r.Coefficient)
                    .ToListAsync();

                return await result;
            }
        }

        public async Task<IList<int>> GetUsedIngredientIds(int recipeId)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.IngredientUsages
                    .Where(iu => recipeId == iu.RecipeId)
                    .Select(k => k.IngredientId)
                    .ToListAsync();

                return result;
            }
        }
    }
}