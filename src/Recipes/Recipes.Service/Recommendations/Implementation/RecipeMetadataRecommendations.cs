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

            var recipes = await RecipesRepository.GetRecipesAsync(recipeTFIDFValues.Keys.ToList());
            var candidates = recipes
                .Select(r => Mapper.Map<RecipeRecommendation>(r))
                .Take(candidatesSize)
                .ToList();

            candidates.ForEach(recommendation => recommendation.RecommenderType = RecommenderType.TfIdf);

            return SelectRecommendationsRandomly(candidates, filter.PageSize.GetValueOrDefault(10));

        }
    }
}