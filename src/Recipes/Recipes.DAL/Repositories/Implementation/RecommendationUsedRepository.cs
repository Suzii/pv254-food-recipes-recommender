using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories.Implementation
{
    public class RecommendationUsedRepository : IRecommendationUsedRepository
    {
        public RecommendationUsed Save(RecommendationUsed recommendationUsed)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.Add(recommendationUsed);
                dbContext.SaveChanges();

                return result;
            }
        }

        public async Task<IList<RecommendationUsed>> GetRecommendationsUsedAsync(IList<int> ids)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.Where(r => ids.Contains(r.Id)).ToListAsync();

                return await result;
            }
        }

        public async Task<RecommendationUsed> GetRecommendationUsedAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.SingleOrDefaultAsync(r => id == r.Id);

                return await result;
            }
        }

        public async Task<IList<RecommendationUsed>> GetAllAsync()
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.ToListAsync();

                return await result;
            }
        }

        public async Task<RecommendationUsed> GetRecommendationsUsedForRecipe(int recipeId)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.SingleOrDefaultAsync(r => recipeId == r.DisplayedRecipeId);

                return await result;
            }
        }

        public async Task<IList<RecommendationUsed>> GetRecommendationsUsedForRecipes(IList<int> recipeIds)
        {
            using (var dbContext = new AppContext())
            {
                var result = dbContext.RecommendationUseds.Where(r => recipeIds.Contains(r.DisplayedRecipeId)).ToListAsync();

                return await result;
            }
        }
    }
}
