using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Helpers;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Implementation
{
    public class IngredientRecommendations : BaseRecommendations, IIngredientRecommendations
    {
        private readonly IIngredientUsagesRepository _usagesRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        private readonly IMapper _mapper;

        public IngredientRecommendations(
            IRecipesRepository recipesRepository,
            IIngredientUsagesRepository usagesRepository,
            IIngredientsRepository ingredientsRepository,
            IMapper mapper) 
            : base(recipesRepository)
        {
            _usagesRepository = usagesRepository;
            _ingredientsRepository = ingredientsRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Get method for recommending similar recipes.
        /// </summary>
        public async Task<IList<RecipeRecommendation>> Get(IngredientBasedFilter filter, int recipeId)
        {
            // Get ingredients for given recipe
            var ingredients = await _usagesRepository.GetUsedIngredientIds(recipeId);

            // Get rid of 'other' ingredients
            var otherIngredientId = await _ingredientsRepository.GetIdByNameAsync("other");
            var filteredIngredients = ingredients.Where(i => i != otherIngredientId).ToList();

            // If all ingredients were labeled as 'other', return empty list
            if (filteredIngredients.Count == 0)
                return new List<RecipeRecommendation>();

            // Get recommendations based on dice coefficient
            return await GetRecommendationsByDiceCoefficient(filter, filteredIngredients, recipeId);
        }

        /// <summary>
        /// Get method for recommending based on ingredients chosen by user.
        /// </summary>
        public async Task<IList<RecipeRecommendation>> Get(IngredientBasedFilter filter)
        {
            // Get rid of 'other' ingredients
            var otherIngredientId = await _ingredientsRepository.GetIdByNameAsync("other");
            var filteredIngredients = filter.IngredientIds.Where(i => i != otherIngredientId).ToList();

            // If all ingredients were labeled as 'other', return empty list
            if (filteredIngredients.Count == 0)
                return new List<RecipeRecommendation>();

            // Get recommendations based on dice coefficient
            return await GetRecommendationsByDiceCoefficient(filter, filteredIngredients);
        }

        private async Task<IList<RecipeRecommendation>> GetRecommendationsByDiceCoefficient
            (IngredientBasedFilter filter, IList<int> filteredIngredients, int? recipeId = null, bool random = true)
        {
            // Get the coefficients
            var diceCoefficients = await _usagesRepository.GetDiceCoefficients(filteredIngredients);

            const int candidatesSize = 50; //TODO decide
            var candidates = new List<RecipeRecommendation>(candidatesSize);

            // Choose recommendations that suit all the constraints
            foreach (var coef in diceCoefficients)
            {
                var id = coef.RecipeId;

                // Do not recommend the same recipe
                if (id == recipeId.GetValueOrDefault(-1))
                    continue;

                // Check the total time and isVegetarian
                var recipe = await RecipesRepository.GetSingleRecipeAsync(id);
                if (recipe.PrepTimeInMinutes + recipe.CookTimeInMinutes
                    <= filter.TotalTimeTo.GetValueOrDefault(int.MaxValue) && 
                    !(filter.IsVegetarian && !recipe.IsVegetarian))
                {
                    var recommendation = _mapper.Map<RecipeRecommendation>(recipe);
                    recommendation.RecommenderType = RecommenderType.IngredientBased;
                    candidates.Add(recommendation);
                }

                if (candidates.Count == candidatesSize)
                    break;
            }

            return random ? GetRecommendationsRandomly(candidates, filter.PageSize.GetValueOrDefault(10)) 
                : candidates.Take(filter.PageSize.GetValueOrDefault(10)).ToList();
        }
    }
}