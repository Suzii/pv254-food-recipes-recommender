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
            throw new System.NotImplementedException();
        }
    }
}