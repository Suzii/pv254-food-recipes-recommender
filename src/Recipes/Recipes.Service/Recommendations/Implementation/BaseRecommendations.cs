using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Repositories;

namespace Recipes.Service.Recommendations.Implementation
{
    public class BaseRecommendations
    {
        protected readonly IRecipesRepository RecipesRepository;

        public BaseRecommendations(IRecipesRepository recipesRepository)
        {
            RecipesRepository = recipesRepository;
        }

        /// <summary>
        /// Gets ids of all recipes
        /// </summary>
        /// <returns>List of recipes' ids.</returns>
        protected async Task<IList<int>> GetAllRecipeIds()
        {
            var allIds = await RecipesRepository.GetAllIdsAsync();
            return allIds;
        }

        /// <summary>
        /// Gets random sub selection of size <param name="count"></param> of recipes' ids
        /// that are going to be used for recommendation selection.
        /// </summary>
        /// <param name="count">Upper bound of number of random ids to be returned</param>
        /// <returns>List of random recipes' ids.</returns>
        protected async Task<IList<int>> GetRandomRecipeIds(int count)
        {
            var allIds = await GetAllRecipeIds();

            var randomSequence = new Random();
            var result =  allIds
                .OrderBy(id => randomSequence.Next())
                .Take(count)
                .ToList();

            return result;
        }
    }
}
