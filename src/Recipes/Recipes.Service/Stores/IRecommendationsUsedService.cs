using System.Threading.Tasks;
using Recipes.Service.DTOs.UserActivity;

namespace Recipes.Service.Stores
{
    public interface IRecommendationsUsedService
    {
        Task<RecommendationUsed> GetAsync(int id);

        RecommendationUsed Save(RecommendationUsed recommendationUsed);
    }
}