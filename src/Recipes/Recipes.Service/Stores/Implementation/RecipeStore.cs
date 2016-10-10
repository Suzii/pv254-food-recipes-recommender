using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs;

namespace Recipes.Service.Stores.Implementation
{
    public class RecipeStore : IRecipeStore
    {
        private readonly IRecipesRepository _recipesRepository;

        private readonly IMapper _mapper;

        public RecipeStore(IRecipesRepository recipesRepository, IMapper mapper)
        {
            _recipesRepository = recipesRepository;
            _mapper = mapper;
        }

        public async Task<Recipe> GetRecipeAsync(int id)
        {
            var recipe = await _recipesRepository.GetFullRecipeAsync(id);
            var recipeDto = _mapper.Map<DTOs.Recipe>(recipe);

            return recipeDto;
        }

        public async Task<List<Recipe>> GetAllRecipeAsync()
        {
            var recipes = await _recipesRepository.GetAllAsync();
            var recipeDtos = recipes.Select(r => _mapper.Map<Recipe>(r)).ToList();
            return recipeDtos;
        }
    }
}
