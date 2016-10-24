using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Fakes
{
    public class BaseRecommendations
    {
        private readonly IRecipesRepository _recipesRepository;

        private readonly IMapper _mapper;


        public BaseRecommendations(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }

        public async Task<IList<RecipeRecommendation>> Get(PagerFilter filter)
        {
            var randomSequence = new Random();
            var allIds = await _recipesRepository.GetAllIdsAsync();
            var randomlySelectedIds =  allIds
                .OrderBy(id => randomSequence.Next())
                .Take(filter.PageSize.GetValueOrDefault(10))
                .ToList();

            var recipeEntities = await _recipesRepository.GetRecipesAsync(randomlySelectedIds);
            var recommendations = recipeEntities
                .Select(r => _mapper.Map<RecipeRecommendation>(r))
                .ToList();

            return recommendations;
        }
    }
}
