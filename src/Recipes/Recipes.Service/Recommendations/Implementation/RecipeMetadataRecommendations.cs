using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Implementation
{
    public class RecipeMetadataRecommendations : BaseRecommendations, IRecipeMetadataRecommendations
    {
        private readonly IRecipesTFIDFRepository _recipesTFIDFRepository;
        public RecipeMetadataRecommendations(IRecipesRepository recipesRepository, IRecipesTFIDFRepository recipesTFIDFRepository, IMapper mapper)
            : base(recipesRepository, mapper)
        {
            _recipesTFIDFRepository = recipesTFIDFRepository;
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            var recommendations = await GetRandomRecommendations(filter.PageSize.GetValueOrDefault(10));

            recommendations.ForEach(r => r.RecommenderType = RecommenderType.RecipeMetadata);

            return recommendations;
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter, int recipeId, int TFIDFnumber = 1)
        {
            var recipeTFIDF = await _recipesTFIDFRepository.GetRecipeTFIDFAsync(recipeId);
            string TFIDFData = "";

            switch(TFIDFnumber)
            {
                case 1:
                    TFIDFData = recipeTFIDF.TFIDF;
                    break;
                case 2:
                    TFIDFData = recipeTFIDF.TFIDF2;
                    break;
                case 3:
                    TFIDFData = recipeTFIDF.TFIDF3;
                    break;
                default:
                    TFIDFData = recipeTFIDF.TFIDF;
                    break;
            }

            Dictionary<int, double> recipeTFIDFValues = new Dictionary<int, double>();
            foreach (var item in TFIDFData.Split(';'))
            {
                var tmp = item.Split(':');
                recipeTFIDFValues.Add(Convert.ToInt32(tmp[0]), Convert.ToDouble(tmp[1]));
            }

            //NOTE: here we could use some threshold, that is the dictionary for (but we do not at the moment)

            const int candidatesSize = 50; //NOTE for now it is same as for Ingredients
            var candidates = new List<RecipeRecommendation>(candidatesSize);

            var recipes = await RecipesRepository.GetRecipesAsync(recipeTFIDFValues.Keys.ToList());
            foreach(var recipe in recipes)
            {
                var recommendation = Mapper.Map<RecipeRecommendation>(recipe);
                recommendation.RecommenderType = RecommenderType.RecipeMetadata;
                candidates.Add(recommendation);

                if (candidates.Count == candidatesSize) break;
            }

            return SelectRecommendationsRandomly(candidates, filter.PageSize.GetValueOrDefault(10));

        }
    }
}