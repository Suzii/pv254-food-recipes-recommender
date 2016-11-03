using System.Threading.Tasks;
using AutoMapper;
using Recipes.DAL.Repositories;
using Recipes.Service.DTOs.UserActivity;

namespace Recipes.Service.Stores.Implementation
{
    // TODO add other methods when necessary
    public class RecommendationsUsedService : IRecommendationsUsedService
    {
        private readonly IRecommendationUsedRepository _recommendationUsedRepository;

        private readonly IMapper _mapper;
        
        public RecommendationsUsedService(IRecommendationUsedRepository recommendationUsedRepository, IMapper mapper)
        {
            _recommendationUsedRepository = recommendationUsedRepository;
            _mapper = mapper;
        }


        public async Task<RecommendationUsed> GetAsync(int id)
        {
            var entity = await _recommendationUsedRepository.GetRecommendationUsedAsync(id);
            var dto = _mapper.Map<DTOs.UserActivity.RecommendationUsed>(entity);

            return dto;
        }

        public RecommendationUsed Save(RecommendationUsed recommendationUsed)
        {
            var entity = _mapper.Map<DAL.Entities.RecommendationUsed>(recommendationUsed);
            var newlyCreatedEntity = _recommendationUsedRepository.Save(entity);
            var newDto = _mapper.Map<DTOs.UserActivity.RecommendationUsed>(newlyCreatedEntity);

            return newDto;
        }
    }
}