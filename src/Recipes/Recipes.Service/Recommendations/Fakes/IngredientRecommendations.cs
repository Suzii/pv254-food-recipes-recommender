using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Helpers;
using Recipes.DAL.Repositories;
using Recipes.DAL.Repositories.Implementation;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Fakes
{
    /// <summary>
    /// Fake implementation for <see cref="IIngredientRecommendations"/>
    /// 
    /// See how Castle Dependency injection is configured in Recipes.Web.CastleInstallers.ServiceInstallers
    /// </summary>
    public class IngredientRecommendations : IIngredientRecommendations
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IIngredientUsagesRepository _usagesRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        private readonly IMapper _mapper;

        public IngredientRecommendations(
            IRecipesRepository recipesRepository, 
            IIngredientUsagesRepository usagesRepository, 
            IngredientRepository ingredientsRepository,
             IMapper mapper)
        {
            _recipesRepository = recipesRepository;
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
            return await DiceCoefficient(filter, filteredIngredients, false, recipeId);
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
            return await DiceCoefficient(filter, filteredIngredients);
        }

        private async Task<IList<RecipeRecommendation>> DiceCoefficient(IngredientBasedFilter filter, 
            IList<int> filteredIngredients, bool random = true, int? recipeId = null)
        {
            // Get the coefficients
            var diceCoefficients = await GetCoefficients(filteredIngredients, random);

            var recommendations = new List<RecipeRecommendation>(filter.PageSize.GetValueOrDefault(10));

            // Choose recommendations that suit all the constraints
            foreach (var coef in diceCoefficients)
            {
                var id = coef.RecipeId;

                // Do not recommend the same recipe
                if (id == recipeId.GetValueOrDefault(-1))
                    continue;

                // Check the total time
                var recipe = await _recipesRepository.GetSingleRecipeAsync(id);
                if (recipe.PrepTimeInMinutes + recipe.CookTimeInMinutes 
                    <= filter.TotalTimeTo.GetValueOrDefault(int.MaxValue))
                {
                    recommendations.Add(_mapper.Map<RecipeRecommendation>(recipe));
                }

                if (recommendations.Count == filter.PageSize.GetValueOrDefault(10))
                    break;
            }

            return recommendations;
        }

        private async Task<List<DiceCoefficientHelper>> GetCoefficients(IList<int> filteredIngredients, 
            bool random)
        {
            List<DiceCoefficientHelper> diceCoefficients;
            if (random)
            {
                var recipeIds = await GetRecipeIds();
                diceCoefficients = await _usagesRepository
                    .GetDiceCoefficients(filteredIngredients, recipeIds);
            }
            else
            {
                diceCoefficients = await _usagesRepository
                    .GetDiceCoefficients(filteredIngredients);
            }
            return diceCoefficients;
        }

        /// <summary>
        /// Gets ids of all recipes that are going to be used for recommendation selection.
        /// </summary>
        /// <param name="random">Determines whether recommendations
        ///  should be selected from random subset of recipes</param>
        /// <returns>List of recipes' ids.</returns>
        private async Task<IList<int>> GetRecipeIds(bool random = false)
        {
            var allIds = await _recipesRepository.GetAllIdsAsync();
            if (!random)
            {
                return allIds;
            }

            var randomSequence = new Random();
            return allIds
                .OrderBy(id => randomSequence.Next())
                .Take(5000) //TODO Decide how many.
                .ToList();
        }
    }
}