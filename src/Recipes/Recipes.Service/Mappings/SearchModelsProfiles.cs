using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class SearchModelsProfiles : Profile
    {
        public SearchModelsProfiles()
        {
            CreateMap<DAL.Helpers.RecipeName, DTOs.SearchModels.RecipeName>();
        }
    }
}