using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;
using Recipes.Service.Recommendations;
using Recipes.Service.Stores.Implementation;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class SimilarRecipesController : ApiController
    {
        //private readonly IRecipeMetadataRecommendations _metadataRecommendations;
        private readonly IIngredientRecommendations _ingredientRecommendations;

        /* 
         public SimilarRecipesController(IRecipeMetadataRecommendations metadataRecommendations)
         {
             _metadataRecommendations = metadataRecommendations;
         }

         // GET: api/recommendations/SimilarRecipes
         public async Task<RecipeRecommendation[]> Get(int currentRecipeId, int pageSize = 10, int pageNumber = 1)
         {
             var filter = new RecipeMetadataBasedFilter
             {
                 RecipeId = currentRecipeId,
                 PageNumber = pageNumber,
                 PageSize = pageSize
             };

             var result = await _metadataRecommendations.Get(filter);

             return result.ToArray();
         }
         */

        public SimilarRecipesController(IIngredientRecommendations ingredientRecommendations)
        {
            _ingredientRecommendations = ingredientRecommendations;
        }

        // GET: api/recommendations/SimilarRecipes
        public async Task<RecipeRecommendation[]> Get(int currentRecipeId, int pageSize = 10, int pageNumber = 1)
        {
            var filter = new IngredientBasedFilter()
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            var result = await _ingredientRecommendations.Get(filter, currentRecipeId);

            return result.ToArray();
        }
    }
}
