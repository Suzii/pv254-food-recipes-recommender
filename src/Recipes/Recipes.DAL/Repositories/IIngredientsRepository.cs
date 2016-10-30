using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories
{
    public interface IIngredientsRepository
    {
        Ingredient Save(Ingredient ingredient);

        Task<Ingredient> GetAsync(int id);

        Task<List<Ingredient>> GetAllAsync();

        Task<Ingredient> GetByNameAsync(string name);

        Task<int> GetIdByNameAsync(string name);
    }
}
