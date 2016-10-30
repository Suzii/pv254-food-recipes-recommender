﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

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
    }
}
