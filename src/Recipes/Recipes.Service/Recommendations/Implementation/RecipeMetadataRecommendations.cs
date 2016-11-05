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
        public RecipeMetadataRecommendations(IRecipesRepository recipesRepository, IMapper mapper)
            : base(recipesRepository, mapper)
        {
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            var recommendations = await GetRandomRecommendations(filter.PageSize.GetValueOrDefault(10));

            recommendations.ForEach(r => r.RecommenderType = RecommenderType.RecipeMetadata);

            return recommendations;
        }
    }
}