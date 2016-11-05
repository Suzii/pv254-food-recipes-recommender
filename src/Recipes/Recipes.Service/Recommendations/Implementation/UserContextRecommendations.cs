using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.Core.Models;
using Recipes.DAL.Helpers;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;
using Recipe = Recipes.DAL.Entities.Recipe;

namespace Recipes.Service.Recommendations.Implementation
{
    public class UserContextRecommendations : BaseRecommendations, IUserContextRecommendations
    {
        // Completely random, empirically selected constants
        private const int NumberOfVisitedRecipesForStatisticalSignificance = 4;
        private const double VegetarianThreshold = 0.65;
        private const double IntervalThreshold = 0.6;
        private const double ChefsThreshold = 0.6;


        public UserContextRecommendations(IRecipesRepository recipesRepository, IMapper mapper)
            : base(recipesRepository, mapper)
        {
        }

        public async Task<IList<RecipeRecommendation>> Get(ContextBasedFilter filter)
        {
            var numberOfResults = filter.PageSize.GetValueOrDefault(10);

            if (filter.RecipeIds.Count < NumberOfVisitedRecipesForStatisticalSignificance)
            {
                var randomRecommendations = await GetRandomRecommendations(numberOfResults);
                SetRecommenderType(randomRecommendations);
                return randomRecommendations;
            }

            var visitedRecipes = await GetVisistedRecipesAsync(filter);
            var onlyVegetarian = ShouldFilterOnlyVegetarian(visitedRecipes);
            var significantCookingInterval = GetSignificantCookingInterval(visitedRecipes);
            var significantChefs = GetSignificantChefs(visitedRecipes);

            var recommendedRecipes = await RecipesRepository
                .GetRecipesAsync(filter.RecipeIds, onlyVegetarian, significantCookingInterval, significantChefs);
            var recommendations = recommendedRecipes.Select(r => Mapper.Map<RecipeRecommendation>(r)).ToList();
            SetRecommenderType(recommendations);

            if (recommendations.Count <= numberOfResults)
            {
                var randomToFillUp = await GetRandomRecommendations(numberOfResults - recommendations.Count);
                SetRecommenderType(randomToFillUp);
                var result = recommendations.Concat(randomToFillUp).ToList();
                return result;
            }

            return SelectRecommendationsRandomly(recommendations, numberOfResults);
        }

        private IList<string> GetSignificantChefs(IList<Recipe> visitedRecipes)
        {
            var chefsCount = visitedRecipes.GroupBy(r => r.Chef).OrderByDescending(gr => gr.Count()).ToDictionary(gr => gr.Key, gr => gr.Count());

            var totalCount = visitedRecipes.Count;
            var significantCount = Math.Ceiling(totalCount*ChefsThreshold);

            var cummulativeSum = 0;
            var significantChefs = chefsCount.TakeWhile(chef => (cummulativeSum += chef.Value) <= significantCount).ToList();
            if (significantChefs.Count > visitedRecipes.Count/2.0)
            {
                return new List<string>();
            }

            var result = significantChefs.Select(gr => gr.Key).ToList();
            return result;
        }

        private MinutesInterval? GetSignificantCookingInterval(IList<Recipe> visitedRecipes)
        {
            var intervalsHistogram = new Dictionary<MinutesInterval, int>()
            {
                { new MinutesInterval(0, 30), 0 },
                { new MinutesInterval(31, 60), 0 },
                { new MinutesInterval(61, 120), 0 },
                { new MinutesInterval(121, null), 0 }
            };

            // creates histogram of time intervals from visited recipes
            var intervalsHistogramKeys = new List<MinutesInterval>(intervalsHistogram.Keys);
            foreach (var interval in intervalsHistogramKeys)
            {
                var count =
                        visitedRecipes.Count(
                            r =>
                                r.GetTotalTimeInMinutes() >= interval.Start.GetValueOrDefault(Int32.MinValue) &&
                                r.GetTotalTimeInMinutes() <= interval.End.GetValueOrDefault(Int32.MaxValue));

                intervalsHistogram[interval] = count;
            }

            //Gets the key-value pair for biggest value
            var mostCommonInterval = intervalsHistogram.Aggregate((l, r) => (l.Value > r.Value)? l : r);
            var mostCommonIntervalPercentage = mostCommonInterval.Value/(double)visitedRecipes.Count;

            if (mostCommonIntervalPercentage < IntervalThreshold)
            {
                return null;
            }

            return mostCommonInterval.Key;
        }

        private bool ShouldFilterOnlyVegetarian(IList<Recipe> visitedRecipes)
        {
            var totalCount = visitedRecipes.Count;
            double totalVegetarianCount = visitedRecipes.Count(r => r.IsVegetarian);
            var vegetarianPercentage = totalVegetarianCount/totalCount;

            return vegetarianPercentage >= VegetarianThreshold;
        }

        private async Task<IList<Recipe>> GetVisistedRecipesAsync(ContextBasedFilter filter)
        {
            return await RecipesRepository.GetRecipesAsync(filter.RecipeIds);
        }

        private void SetRecommenderType(List<RecipeRecommendation> recommendations)
        {
            recommendations.ForEach(r => r.RecommenderType = RecommenderType.UserContext);
        }
    }
}