using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;
using Recipes.Service.Recommendations;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class SimilarRecipesController : ApiController
    {
        private readonly IRecipeMetadataRecommendations _metadataRecommendations;

        public SimilarRecipesController(IRecipeMetadataRecommendations metadataRecommendations)
        {
            _metadataRecommendations = metadataRecommendations;
        }

        // GET: api/recommendations/SimilarRecipes
        public async Task<RecipeRecommendation[]> Get(int currentRecipeId)
        {
            var filter = new RecipeMetadataBasedFilter
            {
                RecipeId = currentRecipeId
            };

            var result = await _metadataRecommendations.Get(filter);

            return result.ToArray();
        }
    }
}
