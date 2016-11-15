using Recipes.DAL.Entities;
using Recipes.DAL.Repositories;
using System.Threading.Tasks;

namespace Recipes.Import.Services.Implementation
{
    internal class IngredientStore: IIngredientStore
    {
        private IIngredientsRepository _ingredientsRepository;

        public IngredientStore(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        public async Task<int> GetOrSaveAsync(Ingredient ingredient)
        {
            var foundIngredient = await _ingredientsRepository.GetByNameAsync(ingredient.Name);
            if (foundIngredient != null)
            {
                return foundIngredient.Id;
            }

            return _ingredientsRepository.Save(ingredient).Id;
        }
    }
}