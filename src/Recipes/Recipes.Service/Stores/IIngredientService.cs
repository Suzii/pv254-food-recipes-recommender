using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;

namespace Recipes.Service.Stores
{
    public interface IIngredientService
    {
        Task<Ingredient> GetAsync(int id);

        Task<Ingredient> GetAsync(string name);

        Task<IEnumerable<Ingredient>> SearchAsync(string name);

        Task<IEnumerable<Ingredient>> GetAllAsync();
    }
}