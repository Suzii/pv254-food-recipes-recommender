using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories.Implementation
{
    public class IngredientRepository : IIngredientsRepository
    {
        public Ingredient Save(Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ingredient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
