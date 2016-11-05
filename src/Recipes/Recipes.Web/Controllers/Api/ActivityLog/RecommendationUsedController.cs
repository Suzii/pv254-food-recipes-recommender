using System;
using System.Threading.Tasks;
using System.Web.Http;
using Recipes.Service.Stores;

namespace Recipes.Web.Controllers.Api.ActivityLog
{
    public class RecommendationUsedController : ApiController
    {
        private readonly IRecommendationsUsedService _recommendationsUsedService;

        public RecommendationUsedController(IRecommendationsUsedService recommendationsUsedService)
        {
            _recommendationsUsedService = recommendationsUsedService;
        }

        // GET: api/activityLog/RecommendationUsed/42
        public async Task<IHttpActionResult> Get(int id)
        {
            var dto = await _recommendationsUsedService.GetAsync(id);

            return Ok(dto);
        }

        // POST: api/activityLog/RecommendationUsed
        public IHttpActionResult Post(Service.DTOs.UserActivity.RecommendationUsed recommendationUsed)
        {
            var createdDto = _recommendationsUsedService.Save(recommendationUsed);

            return Created(new Uri(Url.Link("ActivityLog", new { id = createdDto.Id })), createdDto);
        }
    }
}
