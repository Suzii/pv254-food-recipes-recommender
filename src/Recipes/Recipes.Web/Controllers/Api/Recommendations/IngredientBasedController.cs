using System.Collections.Generic;
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

        // GET: api/Recommendations/IngredientBased?ingredientIds=1&ingredientIds=2
        public async Task<RecipeRecommendation[]> Get([FromUri] int[] ingredientIds, int? totalTimeFrom = null, int? totalTimeTo = null)
        {
            if (ingredientIds == null || ingredientIds.Length == 0)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var filter = new IngredientBasedFilter
            {
                IngredientIds = ingredientIds,
                TotalTimeFrom = totalTimeFrom,
                TotalTimeTo = totalTimeTo,
                PageNumber = 1,
                PageSize = 10
            };

            var result = await _ingredientRecommendations.Get(filter);

            return result.ToArray();
        }
    }
}
