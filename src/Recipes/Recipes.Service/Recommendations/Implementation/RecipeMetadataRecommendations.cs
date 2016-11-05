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
    public class RecipeMetadataRecommendations : IRecipeMetadataRecommendations
    {
        private readonly IRecipesRepository _recipesRepository;
        private readonly IMapper _mapper;

        public RecipeMetadataRecommendations(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }

        public async Task<IList<RecipeRecommendation>> Get(RecipeMetadataBasedFilter filter)
        {
            var randomSequence = new Random();
            var allIds = await _recipesRepository.GetAllIdsAsync();
            var randomlySelectedIds = allIds
                .OrderBy(id => randomSequence.Next())
                .Take(filter.PageSize.GetValueOrDefault(10))
                .ToList();

            var recipeEntities = await _recipesRepository.GetRecipesAsync(randomlySelectedIds);
            var recommendations = recipeEntities
                .Select(r => _mapper.Map<RecipeRecommendation>(r))
                .ToList();

            recommendations.ForEach(r => r.RecommenderType = RecommenderType.RecipeMetadata);

            return recommendations;
        }
    }
}