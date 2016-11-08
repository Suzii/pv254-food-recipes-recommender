using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Repositories.Implementation;

namespace Recipes.TFIDF.Services.Implementation
{
    public class RecipeTFIDFStore : IRecipeTFIDFStore
    {
        private readonly RecipesTFIDFRepository _recipesTFIDFRepository;

        public RecipeTFIDFStore(RecipesTFIDFRepository recipesTFIDFRepository)
        {
            _recipesTFIDFRepository = recipesTFIDFRepository;
        }

        public RecipeTFIDF Save(RecipeTFIDF recipeTDIDF, int TFIDFnumber = 1)
        {
            return _recipesTFIDFRepository.Save(recipeTDIDF, TFIDFnumber);
        }
    }
}
