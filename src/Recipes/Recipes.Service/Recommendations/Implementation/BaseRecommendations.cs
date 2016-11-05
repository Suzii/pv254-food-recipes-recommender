using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs;

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
        /// Gets ids of all recommendations
        /// </summary>
        /// <returns>List of recommendations' ids.</returns>
        protected async Task<IList<int>> GetAllRecipeIds()
        {
            var allIds = await RecipesRepository.GetAllIdsAsync();
            return allIds;
        }

        /// <summary>
        /// Gets random sub selection of size <param name="count"></param> of recommendations' ids
        /// that are going to be used for recommendation selection.
        /// Serves as dummy pre-filtering.
        /// </summary>
        /// <param name="count">Upper bound of number of random ids to be returned</param>
        /// <returns>List of random recommendations' ids.</returns>
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

        /// <summary>
        /// Randomly selects subset of provided <param name="recommendations"></param> of required <param name="size"></param> with respect to original ordering.
        /// Order is considered to be important for the UI as more similar recipes are ranked higher in recommendation list.
        /// Serves as dummy post-filtering.
        /// 
        /// </summary>
        /// <param name="recommendations">Original list of recommendations.</param>
        /// <param name="size">Number of recommendations to be chosen.</param>
        /// <returns>List of recommendations of given size with original relative ordering.</returns>
        protected List<RecipeRecommendation> GetRecommendationsRandomly(IList<RecipeRecommendation> recommendations, int size)
        {
            var randomSequence = new Random();
            var randomlySelectedRecommendations = recommendations
                .OrderBy(id => randomSequence.Next())
                .Take(size)
                .ToList();

            return randomlySelectedRecommendations;
        }
    }
}
