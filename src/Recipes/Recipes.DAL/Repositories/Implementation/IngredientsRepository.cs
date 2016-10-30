using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories.Implementation
{
    public class IngredientsRepository : IIngredientsRepository
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

        public async Task<Ingredient> GetByNameAsync(string name)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.Ingredients
                    .SingleOrDefaultAsync(i => i.Name.Equals(name));

                return result;
            }
        }

        public async Task<int> GetIdByNameAsync(string name)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.Ingredients
                    .Where(i => i.Name.Equals(name))
                    .Select(i => i.Id)
                    .SingleOrDefaultAsync();

                return result;
            }
        }

        public async Task<IEnumerable<Ingredient>> SearchByNameAsync(string name)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.Ingredients
                    .Where(i => i.Name.Contains(name))
                    .ToListAsync();

                return result;
            }
        }
    }
}
