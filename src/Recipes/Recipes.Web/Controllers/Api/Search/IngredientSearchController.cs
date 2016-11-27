using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.Stores;

namespace Recipes.Web.Controllers.Api.Search
{
    public class IngredientSearchController : ApiController
    {
        private readonly IIngredientService _ingredientService;


        public IngredientSearchController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        public async Task<Ingredient[]> Get()
        {
            var ingredients = await _ingredientService.GetAllAsync();

            return ingredients.ToArray();
        }

        public async Task<Ingredient[]> Get(string nameFilter)
        {
            var ingredients = (string.IsNullOrEmpty(nameFilter))? await _ingredientService.GetAllAsync(): await _ingredientService.SearchAsync(nameFilter);

            return ingredients.ToArray();
        }
    }
}
