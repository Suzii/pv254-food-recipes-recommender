using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.Search;

namespace Recipes.Web.Controllers.Api.Search
{
    public class RecipeSearchController : ApiController
    {
        private readonly IRecipeSearch _recipeSearch;

        public RecipeSearchController(IRecipeSearch recipeSearch)
        {
            _recipeSearch = recipeSearch;
        }

        // GET: api/Search/Recipes
        public async Task<IList<Service.DTOs.SearchModels.RecipeName>> Get()
        {
            var recipes = await _recipeSearch.GetAllRecipeNamesAsync();
            return recipes;
        }

        // GET: api/Search/Recipes?q=searchQuery
        public async Task<IList<Service.DTOs.RecipeRecommendation>> Get(string q)
        {
            var recipes = await _recipeSearch.GetRecipeRecommendationsByNamesAsync(q);
            return recipes;
        }
    }
}
