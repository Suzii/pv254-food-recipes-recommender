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
    public class IngredientRecommendations : Implementation.BaseRecommendations, IIngredientRecommendations
    {
        private readonly IIngredientUsagesRepository _usagesRepository;
        private readonly IIngredientsRepository _ingredientsRepository;

        private readonly IMapper _mapper;

        public IngredientRecommendations(
            IRecipesRepository recipesRepository, 
            IIngredientUsagesRepository usagesRepository, 
            IIngredientsRepository ingredientsRepository,
             IMapper mapper) : base(recipesRepository)
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
            return await GetRecommendationsByDiceCoefficient(filter, filteredIngredients, recipeId, false);
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
            var diceCoefficients = await GetDiceCoefficients(filteredIngredients, random);

            var pageSize = filter.PageSize.GetValueOrDefault(10);
            var recommendations = new List<RecipeRecommendation>(pageSize);

            // Choose recommendations that suit all the constraints
            foreach (var coef in diceCoefficients)
            {
                var id = coef.RecipeId;

                // Do not recommend the same recipe
                if (id == recipeId.GetValueOrDefault(-1))
                    continue;

                // Check the total time
                var recipe = await RecipesRepository.GetSingleRecipeAsync(id);
                if (recipe.PrepTimeInMinutes + recipe.CookTimeInMinutes 
                    <= filter.TotalTimeTo.GetValueOrDefault(int.MaxValue))
                {
                    recommendations.Add(_mapper.Map<RecipeRecommendation>(recipe));
                }

                if (recommendations.Count == pageSize)
                    break;
            }

            return recommendations;
        }

        private async Task<List<DiceCoefficientHelper>> GetDiceCoefficients(IList<int> filteredIngredients, bool random)
        {
            // if null, all recipes will be considered
            var recipeIdsSubset = (random) ? await GetRandomRecipeIds(5000) : null;

            var diceCoefficients = await _usagesRepository.GetDiceCoefficients(filteredIngredients, recipeIdsSubset);

            return diceCoefficients;
        }
    }
}