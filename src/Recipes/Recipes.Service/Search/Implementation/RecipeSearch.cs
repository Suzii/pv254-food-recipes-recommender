using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.SearchModels;

namespace Recipes.Service.Search.Implementation
{
    public class RecipeSearch : IRecipeSearch
    {
        private readonly IRecipesRepository _recipesRepository;

        private readonly IMapper _mapper;

        public RecipeSearch(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }
        public async Task<IList<RecipeName>> GetAllRecipeNamesAsync()
        {
            var recipeNames = await _recipesRepository.GetAllNamesAsync();
            var result = recipeNames.Select(rn => _mapper.Map<RecipeName>(rn)).ToList();

            return result;
        }

        public async Task<IList<RecipeRecommendation>> GetRecipeRecommendationsByNamesAsync(string name)
        {
            var recipes = await _recipesRepository.SearchByNameAsync(name);
            var result = recipes.Select(r => _mapper.Map<RecipeRecommendation>(r)).ToList();

            result.ForEach(r => r.RecommenderType = RecommenderType.RecipeSearch);
            return result;
        }
    }
}
