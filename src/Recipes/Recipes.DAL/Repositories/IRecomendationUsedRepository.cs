using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.DAL.Entities;

namespace Recipes.DAL.Repositories
{
    public interface IRecommendationUsedRepository
    {
        RecommendationUsed Save(RecommendationUsed recommendationUsed);

        Task<IList<RecommendationUsed>> GetRecommendationsUsedAsync(IList<int> ids);

        Task<RecommendationUsed> GetRecommendationUsedAsync(int id);

        Task<IList<RecommendationUsed>> GetAllAsync();

        Task<IList<RecommendationUsed>> GetRecommendationsUsedForRecipes(IList<int> recipeIds);

        /// <summary>
        /// Returns all <see cref="RecommendationUsed"/> that have <param name="recipeId"></param> as <c>DisplayedRecipeId</c>
        /// </summary>
        /// <param name="recipeId">Id of recipe that should be looked for</param>
        /// <returns>List of recommendations used with <c>DisplayedRecipeId</c> equal to <param name="recipeId"></param></returns>
        Task<RecommendationUsed> GetRecommendationsUsedForRecipe(int recipeId);

        /// <summary>
        /// Retrieves top <param name="count"></param> recipe ids that have most clicks.
        /// </summary>
        /// <param name="count">Number of ids to be returned</param>
        /// <returns>Ids of most popular recipes</returns>
        Task<IList<int>> GetMostClickedRecipesIds(int count);
    }
}
