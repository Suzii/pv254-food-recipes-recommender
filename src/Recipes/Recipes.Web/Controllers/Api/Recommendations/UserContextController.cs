using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;
using Recipes.Service.Recommendations;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class UserContextController : ApiController
    {
        private readonly IUserContextRecommendations _userContextRecommendations;

        public UserContextController(IUserContextRecommendations userContextRecommendations)
        {
            _userContextRecommendations = userContextRecommendations;
        }

        // TODO add more parameters to the request
        // GET: api/reecommendations/UserContext
        public async Task<RecipeRecommendation[]> Get(int currentRecipeId, int pageSize = 10, int pageNumber = 1)
        {
            var filter = new ContextBasedFilter
            {
                RecipeId = currentRecipeId,
                PageNumber = pageNumber, 
                PageSize = pageSize
            };

            var result = await _userContextRecommendations.Get(filter);

            return result.ToArray();
        }
    }
}
