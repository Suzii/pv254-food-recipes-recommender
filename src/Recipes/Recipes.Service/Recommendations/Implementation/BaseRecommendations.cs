using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs;

namespace Recipes.Service.Recommendations.Implementation
{
    public class BaseRecommendations
    {
        protected readonly IRecipesRepository RecipesRepository;

        protected readonly IMapper Mapper;

        /// <summary>
        /// This constant means how many results are to be send to post-filtering process.
        /// Since most common use case is that we will return 10 recommendations total, and of those 
        /// typically only first 5 are used (merging of 2 lists for A/B testing),
        /// 15 seems as a good trade-off between on randomness and relevance of provided recommendations.
        /// </summary>
        protected const int CANDIDATES_COUNT = 15;


        public BaseRecommendations(IRecipesRepository recipesRepository, IMapper mapper)
        {
            RecipesRepository = recipesRepository;
            Mapper = mapper;
        }

        /// <summary>
        /// Gets random sub selection of size <param name="count"></param> of recommendations' ids
        /// that are going to be used for recommendation selection.
        /// Serves as dummy pre-filtering.
        /// </summary>
        /// <param name="count">Upper bound of number of random ids to be returned</param>
        /// <returns>List of random recommendations' ids.</returns>
        protected async Task<List<int>> GetRandomRecipeIds(int count)
        {
            var allIds = await GetAllRecipeIds();

            var randomSequence = new Random();
            var result =  allIds
                .OrderBy(id => randomSequence.Next())
                .Take(count)
                .ToList();

            return result;
        }

        protected async Task<List<RecipeRecommendation>> GetRandomRecommendations(int count)
        {
            var randomlySelectedIds = await GetRandomRecipeIds(count);
            var recipeEntities = await RecipesRepository.GetRecipesAsync(randomlySelectedIds);
            var recommendations = recipeEntities
                .Select(r => Mapper.Map<RecipeRecommendation>(r))
                .ToList();

            return recommendations;
        }

        /// <summary>
        /// Randomly selects subset of provided <param name="recommendations"></param> of required <param name="size"></param> with respect to original ordering.
        /// Order is considered to be important for the UI as more similar recipes are ranked higher in recommendation list.
        /// Serves as dummy post-filtering. 
        /// </summary>
        /// <param name="recommendations">Original list of recommendations.</param>
        /// <param name="size">Number of recommendations to be chosen.</param>
        /// <returns>List of recommendations of given size with original relative ordering.</returns>
        protected List<RecipeRecommendation> SelectRecommendationsRandomly(IList<RecipeRecommendation> recommendations, int size)
        {
            var randomSequence = new Random();
            var randomlySelectedIds = recommendations
                .Select(r => r.Id)
                .OrderBy(id => randomSequence.Next())
                .Take(size)
                .ToList();

            var randomRecommendationsWIthRelativeOrdering = recommendations
                .Where(r => randomlySelectedIds.Contains(r.Id))
                .ToList();
            
            return randomRecommendationsWIthRelativeOrdering;
        }

        /// <summary>
        /// Gets ids of all recommendations
        /// </summary>
        /// <returns>List of recommendations' ids.</returns>
        private async Task<IList<int>> GetAllRecipeIds()
        {
            var allIds = await RecipesRepository.GetAllIdsAsync();
            return allIds;
        }
    }
}
