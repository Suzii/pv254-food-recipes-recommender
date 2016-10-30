using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.Stores;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class IngredientsController : ApiController
    {
        private readonly IIngredientService _ingredientService;


        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
        
        public async Task<Ingredient[]> Get(string nameFilter)
        {
            var ingredients = (string.IsNullOrEmpty(nameFilter))? await _ingredientService.GetAllAsync(): await _ingredientService.SearchAsync(nameFilter);

            return ingredients.ToArray();
        }
    }
}
