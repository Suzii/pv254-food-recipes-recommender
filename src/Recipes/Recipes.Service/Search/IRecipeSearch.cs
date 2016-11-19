using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs.SearchModels;

namespace Recipes.Service.Search
{
    public interface IRecipeSearch
    {
        Task<IList<RecipeName>> GetAllRecipeNamesAsync();
    }
}
