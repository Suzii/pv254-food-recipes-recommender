using Recipes.DAL.Entities;
using Recipes.DAL.Repositories;

namespace Recipes.Import.Services.Implementation
{
    internal class RecipeStore :IRecipeStore
    {
        private readonly IRecipesRepository _recipesRepository;

        public RecipeStore(IRecipesRepository recipesRepository)
        {
            _recipesRepository = recipesRepository;
        }

        public int SaveRecipe(Recipe recipe)
        {
            throw new System.NotImplementedException();
        }
    }
}