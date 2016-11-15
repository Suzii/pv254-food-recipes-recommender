using Recipes.DAL.Entities;
using System.Threading.Tasks;

namespace Recipes.DAL.Repositories
{
    public interface IRecipesTFIDFRepository
    {
        Task<RecipeTFIDF> GetRecipeTFIDFAsync(int id);

        RecipeTFIDF Save(RecipeTFIDF recipeTFIDF, int TFIDFnumber = 1);
    }
}
