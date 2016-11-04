using System;
using Recipes.DAL.Repositories.Implementation;
using Recipes.Import.Services.Implementation;

namespace Recipes.Import
{
    class Program
    {
        private const string DIRECTORY = "..\\..\\..\\..\\..\\data\\";

        static void Main(string[] args)
        {
            // NOTE : I know this is totally terrible but I did not figure out yet how to set up dependency injection in console application
            var ingredientsRepository = new IngredientsRepository();
            var recipesRepository = new RecipesRepository();
            var ingredientStore = new IngredientStore(ingredientsRepository);
            var recipesStore = new RecipeStore(recipesRepository);
            var importer = new Services.Implementation.Importer(ingredientStore, recipesStore);

            Console.WriteLine("Import started...");
            Console.WriteLine("Brace yourselves, this might take ages...");

            importer.ImportAllRecipes(DIRECTORY);

            Console.WriteLine("Importing finished successfully.");
            Console.ReadKey();
        }
    }
}
