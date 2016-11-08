using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Services
{
    public interface IRecipeStore
    {
        Task<IList<int>> GetAllRecipesIdsAsync();

        Task<Recipe> GetSingleRecipeAsync(int id);

        Task<IList<Recipe>> GetRecipesAsync(IList<int> ids);

        Task<IList<RecipeDocumentHelper>> GetAllRecipeDocumentsAsync(int titlesRepeat = 1);
    }
}
