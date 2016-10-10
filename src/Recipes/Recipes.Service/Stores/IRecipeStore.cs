using System.Collections.Generic;
using System.Threading.Tasks;

namespace Recipes.Service.Stores
{
    public interface IRecipeStore
    {
        Task<DTOs.Recipe> GetRecipeAsync(int id);

        Task<List<DTOs.Recipe>> GetAllRecipeAsync();
    }
}
