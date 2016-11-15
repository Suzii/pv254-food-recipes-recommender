using System.Collections.Generic;
using System.IO;
using System.Linq;
using Recipes.DAL.Entities;
using Recipes.Import.Parser;

namespace Recipes.Import.Services.Implementation
{
    public class Importer : IImporter
    {
        private readonly IIngredientStore _ingredientStore;
        private readonly IRecipeStore _recipesStore;

        public Importer(IIngredientStore ingredientStore, IRecipeStore recipesStore)
        {
            _ingredientStore = ingredientStore;
            _recipesStore = recipesStore;
        }

        public void ImportAllRecipes(string directory)
        {
            var parser = new XmlRecipeParser();
            var fileNames = GetAllRecipeFileNames(directory);

            foreach (var fileName in fileNames)
            {
                var recipe = parser.ParseFile(fileName);
                ImportIngredientsAsync(recipe);

                _recipesStore.SaveRecipe(recipe);
            }
        }

        private async void ImportIngredientsAsync(Recipe recipe)
        {
            foreach (var ingredientUsage in recipe.IngredientUsages)
            {
                var id = await _ingredientStore.GetOrSaveAsync(ingredientUsage.Ingredient);
                ingredientUsage.IngredientId = id;
            }
        }

        private IEnumerable<string> GetAllRecipeFileNames(string directory)
        {
            return Directory.GetFiles(directory).Where(file => file.EndsWith(".html"));
        }
    }
}
