using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.Stores;

namespace Recipes.Web.Controllers.Api
{
    public class RecipesController : ApiController
    {
        private readonly IRecipeStore _recipeStore;

        public RecipesController(IRecipeStore recipeStore)
        {
            _recipeStore = recipeStore;
        }

        // GET: api/Recipes
        public async Task<List<Service.DTOs.Recipe>> Get()
        {
            var recipes = await _recipeStore.GetAllRecipeAsync();
            return recipes;
        }

        // GET: api/Recipes/5
        public async Task<Service.DTOs.Recipe> Get(int id)
        {
            var recipe = _recipeStore.GetRecipeAsync(id);
            return await recipe;
        }

        // POST: api/Recipes
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Recipes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Recipes/5
        public void Delete(int id)
        {
        }
    }
}
