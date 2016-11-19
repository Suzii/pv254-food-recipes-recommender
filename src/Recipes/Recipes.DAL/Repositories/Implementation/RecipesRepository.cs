using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Recipes.Core.Models;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;
using System.Data.Entity.SqlServer;

namespace Recipes.DAL.Repositories.Implementation
{
    public class RecipesRepository : IRecipesRepository
    {
        public Recipe Save(Recipe recipe)
        {
            using (var dbContext = new AppContext())
            {
                // NOTE: this needs to be done in order to prevent EF
                // from trying to insert ingredients more times
                foreach (var ingredientUsage in recipe.IngredientUsages)
                {
                    ingredientUsage.Ingredient = null;
                }
                var result = dbContext.Recipes.Add(recipe);
                dbContext.SaveChanges();

                return result;
            }
        }

        public async Task<Recipe> GetFullRecipeAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Include(r => r.IngredientUsages.Select(iu => iu.Ingredient))
                    .SingleAsync(r => r.Id == id);

                return await result;
            }
        }

        public async Task<Recipe> GetSingleRecipeAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.Recipes
                    .SingleOrDefaultAsync(r => r.Id == id);

                return result;
            }
        }

        public async Task<IList<Recipe>> GetRecipesAsync(IList<int> ids)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Where(r => ids.Contains(r.Id))
                    .ToListAsync();

                return await result;
            }
        }

        public async Task<IList<Recipe>> GetRecipesAsync(IList<int> excludedRecipeIds, bool onlyVegetarian, MinutesInterval? totalTimeInterval, IList<string> chefs)
        {
            using (var appContext = new AppContext())
            {
                var recipes = appContext.Recipes.Where(r => !excludedRecipeIds.Contains(r.Id));
                if (onlyVegetarian)
                {
                    recipes = recipes.Where(r => r.IsVegetarian);
                }

                if (totalTimeInterval != null)
                {
                    var moreThan = totalTimeInterval.Value.Start.GetValueOrDefault();
                    var lessThan = totalTimeInterval.Value.End.GetValueOrDefault(int.MaxValue);
                    recipes = recipes.Where(r =>
                        (r.CookTimeInMinutes + r.PrepTimeInMinutes) >= moreThan
                        && (r.CookTimeInMinutes + r.PrepTimeInMinutes) <= lessThan);
                }

                if (chefs != null && chefs.Count > 0)
                {
                    recipes = recipes.Where(r => chefs.Contains(r.Chef));
                }

                return await recipes.ToListAsync();
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

        public async Task<IList<int>> GetAllIdsAsync()
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Select(r => r.Id)
                    .ToListAsync();

                return await result;
            }
        }

        public async Task<IList<RecipeName>> GetAllNamesAsync()
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes
                    .Select(r => new RecipeName
                    {
                        Id = r.Id,
                        Name = r.Title
                    })
                    .ToListAsync();

                return await result;
            }
        }

        public async Task<IList<RecipeDocumentHelper>> GetAllRecipeDocumentsAsync(int titlesRepeat = 1)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Recipes.Select(r => new RecipeDocumentHelper
                {
                    RecipeId = r.Id,
                    Document = SqlFunctions.Replicate(r.Title + "#", titlesRepeat) + r.Instructions

                }).ToListAsync();
                return await result;
            }

        }
    }
}
