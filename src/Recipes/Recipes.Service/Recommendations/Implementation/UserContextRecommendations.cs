using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.Service.DTOs;
using Recipes.Service.DTOs.Filters;

namespace Recipes.Service.Recommendations.Implementation
{
    public class UserContextRecommendations : IUserContextRecommendations
    {
        public Task<IList<RecipeRecommendation>> Get(ContextBasedFilter filter)
        {
            throw new System.NotImplementedException();
        }
    }
}