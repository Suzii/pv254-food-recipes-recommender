using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;
using Recipes.DAL.Repositories;

namespace Recipes.TFIDF.Services.Implementation
{
    public class RecipeStore : IRecipeStore
    {
        private readonly IRecipesRepository _recipesRepository;

        public RecipeStore(IRecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
        }

        public async Task<IList<RecipeDocumentHelper>> GetAllRecipeDocumentsAsync(int titlesRepeat = 1)
        {
            return await _recipesRepository.GetAllRecipeDocumentsAsync(titlesRepeat);
        }

        public async Task<IList<int>> GetAllRecipesIdsAsync()
        {
            return await _recipesRepository.GetAllIdsAsync();
        }

        public async Task<IList<Recipe>> GetRecipesAsync(IList<int> ids)
        {
            return await _recipesRepository.GetRecipesAsync(ids);
        }

        public async Task<Recipe> GetSingleRecipeAsync(int id)
        {
            return await _recipesRepository.GetSingleRecipeAsync(id);
        }

    }
}
