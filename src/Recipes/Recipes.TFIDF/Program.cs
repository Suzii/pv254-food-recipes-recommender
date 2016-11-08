using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipes.DAL.Repositories.Implementation;
using Recipes.TFIDF.TFIDF;
using Recipes.TFIDF.Services.Implementation;

namespace Recipes.TFIDF
{
    public class Program
    {
        public static void Main(string[] args)
        {
            RecipesRepository recipeRepository = new RecipesRepository();
            RecipesTFIDFRepository recipeTFIDFRepository = new RecipesTFIDFRepository();
            RecipeStore recipeService = new RecipeStore(recipeRepository);
            RecipeTFIDFStore recipeTFIDFStore = new RecipeTFIDFStore(recipeTFIDFRepository);
            TFIDFImporter importer = new TFIDFImporter(recipeTFIDFStore, recipeService);

            Console.WriteLine("This is gonna take a while...");
            //choosing default column TFIDF and 10xtitleRepeat
            importer.ImportTFIDF(10, 100);
            Console.WriteLine("Finally..it's done!");
            Console.ReadLine();

       
        }

    }
}
