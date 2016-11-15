using Recipes.DAL.Entities;
using System.Threading.Tasks;

namespace Recipes.Import.Services
{
    public interface IIngredientStore
    {
        /// <summary>
        /// Looks up ingredient by name in DB.
        /// If exists, returns id. Otherwise maps ingredient to Ingredient entity, 
        /// stores it in DB and returns newly created id.
        /// </summary>
        /// <param name="ingredient">Ingredient to be looked up or stored</param>
        /// <returns>Id of the ingredient</returns>
        Task<int> GetOrSaveAsync(Ingredient ingredient);
    }
}
