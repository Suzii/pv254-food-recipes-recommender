using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
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

                dbContext.SaveChanges();

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

        public Task<Ingredient> GetByNameAsync(string name)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.Ingredients
                    .SingleOrDefaultAsync(i => i.Name.Equals(name));

                return result;
            }
        }
    }
}
