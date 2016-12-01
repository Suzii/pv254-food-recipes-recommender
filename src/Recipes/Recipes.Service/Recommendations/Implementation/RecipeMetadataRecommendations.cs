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
        public const double THRESHOLD = 0.2f;

        private readonly IRecipesTFIDFRepository _recipesTFIDFRepository;
        public RecipeMetadataRecommendations(IRecipesRepository recipesRepository, IRecipesTFIDFRepository recipesTFIDFRepository, IMapper mapper)
            : base(recipesRepository, mapper)
        {
            _recipesTFIDFRepository = recipesTFIDFRepository;
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            var recipeTFIDF = await _recipesTFIDFRepository.GetRecipeTFIDFAsync(filter.RecipeId);
            string TFIDFData = "";

            switch(filter.TFIDFnumber)
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
           
                if(tmp.Length == 2)
                {
                    recipeTFIDFValues.Add(Convert.ToInt32(tmp[0]), Convert.ToDouble(tmp[1]));
                }
            }

            //NOTE: here we could use some threshold, that is the dictionary for (but we do not at the moment)
            var recipeTFIDFValuesWithThreshold = recipeTFIDFValues.Where(x => x.Value >= THRESHOLD).Select(x => x.Key).ToList();
            IList<DAL.Entities.Recipe> recipes = new List<DAL.Entities.Recipe>();
            //if threshold is too high, we take first MINIMUM_COUNT (no option for random)
            if (recipeTFIDFValuesWithThreshold.Count < MINIMUM_COUNT)
            {
                recipes = await RecipesRepository.GetRecipesAsync(recipeTFIDFValues.Take(MINIMUM_COUNT).Select(x => x.Key).ToList());
            }
            else
            {
               recipes = await RecipesRepository.GetRecipesAsync(recipeTFIDFValuesWithThreshold);
            }

            var candidates = recipes
                .Select(r => Mapper.Map<RecipeRecommendation>(r))
                .Take(CANDIDATES_COUNT)
                .ToList();

            candidates.ForEach(recommendation => recommendation.RecommenderType = RecommenderType.TfIdf);

            return SelectRecommendationsRandomly(candidates, filter.PageSize.GetValueOrDefault(MINIMUM_COUNT));

        }
    }
}