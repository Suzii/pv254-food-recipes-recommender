using Recipes.DAL.Entities;
using Recipes.TFIDF.TFIDF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Services.Implementation
{
    public class TFIDFImporter : ITFIDFImporter
    {

        private readonly RecipeTFIDFStore _recipeTFIDFStore;
        private readonly RecipeStore _recipeService;


        public TFIDFImporter(RecipeTFIDFStore recipeTFIDFStore, RecipeStore recipeService)
        {
            _recipeTFIDFStore = recipeTFIDFStore;
            _recipeService = recipeService;
        }
        public async Task ImportTFIDF(int titlesRepeat, int importValuesCount, int TFIDFnumber = 1)
        {
            var recipeDocuments = await _recipeService.GetAllRecipeDocumentsAsync(titlesRepeat);

            var normalizedRecipeVectors = TFIDFHelper.Transform(recipeDocuments);
            recipeDocuments = null;

            List<RecipeTFIDF> similarityDoc = new List<RecipeTFIDF>();

            foreach (var outer in normalizedRecipeVectors)
            {
                Dictionary<int, double> cosSimOfRecipe = new Dictionary<int, double>();
                foreach (var inner in normalizedRecipeVectors)
                {
                    if (outer.Key == inner.Key) continue;
                    var cosSim = TFIDFHelper.CosineSimilarity(outer.Value, inner.Value);
                    cosSimOfRecipe.Add(inner.Key, cosSim);
                }
                string recipeTFIDFValue = "";
                foreach (var item in cosSimOfRecipe.OrderByDescending(x => x.Value).Take(importValuesCount))
                {
                    Console.Write(item.Key + ": " + item.Value + "; ");
                    recipeTFIDFValue += item.Key + ":" + item.Value + ";";
                }
                RecipeTFIDF recipeTFIDF = new RecipeTFIDF();
                recipeTFIDF.RecipeId = outer.Key;
                switch(TFIDFnumber)
                {
                    case 1:
                        recipeTFIDF.TFIDF = recipeTFIDFValue;
                        break;
                    case 2:
                        recipeTFIDF.TFIDF2 = recipeTFIDFValue;
                        break;
                    case 3:
                        recipeTFIDF.TFIDF3 = recipeTFIDFValue;
                        break;
                    default:
                        recipeTFIDF.TFIDF = recipeTFIDFValue;
                        break;
                }
                _recipeTFIDFStore.Save(recipeTFIDF, TFIDFnumber);
            }

        }
    }
}
