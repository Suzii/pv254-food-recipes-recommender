using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.DTOs;
using Recipes.Service.Recommendations;

namespace Recipes.Web.Controllers.Api.Recommendations
{
    public class MostPopularController : ApiController
    {
        private readonly IMostPopularRecommendations _mostPopularRecommendations;

        public MostPopularController(IMostPopularRecommendations mostPopularRecommendations)
        {
            _mostPopularRecommendations = mostPopularRecommendations;
        }


        // GET: api/recommendations/MostPopular
        public async Task<RecipeRecommendation[]> Get(int count)
        {
            var result = await _mostPopularRecommendations.Get(count);

            return result.ToArray();
        }
    }
}
