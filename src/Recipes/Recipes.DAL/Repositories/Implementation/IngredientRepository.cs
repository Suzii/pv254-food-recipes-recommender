﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories.Implementation
{
    public class IngredientRepository : IIngredientsRepository
    {
        public Ingredient Save(Ingredient ingredient)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Ingredients.Add(ingredient);

                return result;
            }
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Ingredients
                    .SingleAsync(i => i.Id == id);

                return  await result;
            }
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Ingredients
                    .ToListAsync();

                return await result;
            }
        }

        public Ingredient Exists(string name)
        {
            //TODO
            return null;
        }
    }
}
