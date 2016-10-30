﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;

namespace Recipes.DAL.Repositories.Implementation
{
    public class IngredientUsagesRepository : IIngredientUsagesRepository
    {

        public async Task<List<DiceCoefficientHelper>> GetDiceCoefficients(IList<int> ingredientIds)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.IngredientUsages
                    .GroupBy(ui => ui.RecipeId)
                    .Select(
                        g => new DiceCoefficientHelper
                        {
                            RecipeId = g.Key,
                            Coefficient = 2 * g.Count(r => ingredientIds.Contains(r.IngredientId))
                                          / (double)(g.Count() + ingredientIds.Count)
                        })
                    .OrderByDescending(r => r.Coefficient)
                    .ToListAsync();

                return result;
            }
        }

        public async Task<List<DiceCoefficientHelper>> GetDiceCoefficientsOnRandomRecipes(IList<int> ingredientIds, IList<int> recipeIds)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.IngredientUsages
                    .Where(r => recipeIds.Contains(r.RecipeId))
                    .GroupBy(ui => ui.RecipeId)
                    .Select(
                        g => new DiceCoefficientHelper
                        {
                            RecipeId = g.Key,
                            Coefficient = 2*g.Count(r => ingredientIds.Contains(r.IngredientId))
                                          / (double)(g.Count() + ingredientIds.Count)
                        })
                    .OrderByDescending(r => r.Coefficient)
                    .ToListAsync();

                return result;
            }
        }

        public async Task<IList<int>> GetIngredientUsagesForRecipe(int recipeId)
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