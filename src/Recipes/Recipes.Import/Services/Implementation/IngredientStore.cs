using Recipes.DAL.Entities;
using Recipes.DAL.Repositories;

namespace Recipes.Import.Services.Implementation
{
    internal class IngredientStore: IIngredientStore
    {
        private IIngredientsRepository _ingredientsRepository;

        public IngredientStore(IIngredientsRepository ingredientsRepository)
        {
            _ingredientsRepository = ingredientsRepository;
        }

        public int GetOrSave(Ingredient ingredient)
        {
            var foundIngredient = _ingredientsRepository.GetByNameAsync(ingredient.Name);
            if (foundIngredient != null)
            {
                return foundIngredient.Id;
            }

            return _ingredientsRepository.Save(ingredient).Id;
        }
    }
}