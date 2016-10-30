using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs;

namespace Recipes.Service.Stores.Implementation
{
    public class IngredientService : IIngredientService
    {
        private readonly IIngredientsRepository _ingredientsRepository;

        private readonly IMapper _mapper;

        public IngredientService(IMapper mapper, IIngredientsRepository ingredientsRepository)
        {
            _mapper = mapper;
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            var ingredient = await _ingredientsRepository.GetAsync(id);
            var ingredientDto = _mapper.Map<DTOs.Ingredient>(ingredient);

            return ingredientDto;
        }

        public async Task<Ingredient> GetAsync(string name)
        {
            var ingredient = await _ingredientsRepository.GetByNameAsync(name);
            var ingredientDto = _mapper.Map<DTOs.Ingredient>(ingredient);

            return ingredientDto;
        }

        public async Task<IEnumerable<Ingredient>> SearchAsync(string name)
        {
            var ingredients = await _ingredientsRepository.SearchByNameAsync(name);
            var ingredientDtos = ingredients.Select(i => _mapper.Map<DTOs.Ingredient>(i));

            return ingredientDtos;
        }

        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            var ingredients = await _ingredientsRepository.GetAllAsync();
            var ingredientDtos = ingredients.Select(i => _mapper.Map<DTOs.Ingredient>(i));

            return ingredientDtos;
        }
    }
}
