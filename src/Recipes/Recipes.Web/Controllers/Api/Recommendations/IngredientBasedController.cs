using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;
using Recipes.Service.Recommendations;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class IngredientBasedController : ApiController
    {
        private readonly IIngredientRecommendations _ingredientRecommendations;

        public IngredientBasedController(IIngredientRecommendations ingredientRecommendations)
        {
            _ingredientRecommendations = ingredientRecommendations;
        }

        // POST: api/Recommendations/IngredientBased?ingredientIds=1&ingredientIds=2
        public async Task<RecipeRecommendation[]> Post(int[] ingredientIds, bool isVegetarian = false, int? totalTimeTo = null, int pageSize = 10, int pageNumber = 1)
        {
            if (ingredientIds == null || ingredientIds.Length == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var filter = new IngredientBasedFilter
            {
                IngredientIds = ingredientIds,
                IsVegetarian = isVegetarian,
                TotalTimeTo = totalTimeTo,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _ingredientRecommendations.Get(filter);

            return result.ToArray();
        }

        // GET: api/Recommendations/IngredientBased?currentRecipeId=60
        public async Task<RecipeRecommendation[]> Get(int currentRecipeId, int pageSize = 10, int pageNumber = 1)
        {
            var filter = new IngredientBasedFilter
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _ingredientRecommendations.Get(filter, currentRecipeId);

            return result.ToArray();
        }
    }
}
