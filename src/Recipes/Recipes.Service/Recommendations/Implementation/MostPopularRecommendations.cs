using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.Constants;
using Recipes.Service.DTOs;

namespace Recipes.Service.Recommendations.Implementation
{
    public class MostPopularRecommendations : BaseRecommendations, IMostPopularRecommendations
    {
        private readonly IRecommendationUsedRepository _recommendationUsedRepository;

        public MostPopularRecommendations(
            IRecipesRepository recipesRepository, 
            IMapper mapper, IRecommendationUsedRepository recommendationUsedRepository) 
            : base(recipesRepository, mapper)
        {
            _recommendationUsedRepository = recommendationUsedRepository;
        }
        public async Task<IList<RecipeRecommendation>> Get(int count)
        {
            var mostPopularIds = await _recommendationUsedRepository.GetMostClickedRecipesIds(count);
            var mostPopularRecipes = await RecipesRepository.GetRecipesAsync(mostPopularIds);
            var result = mostPopularRecipes.Select(r => Mapper.Map<RecipeRecommendation>(r)).ToList();

            result.ForEach(rec => rec.RecommenderType = RecommenderType.MostPopularList);

            return result;
        }
    }
}