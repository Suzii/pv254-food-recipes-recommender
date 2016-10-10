using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class RecipeProfile: Profile
    {
        private static char _instructionsSeparator = '#';

        public RecipeProfile()
        {
            CreateMap<DAL.Entities.Recipe, DTOs.Recipe>()
                .ForMember(dest => dest.Instructions, opt => opt.ResolveUsing((source, destination) => source.Instructions.Split(_instructionsSeparator)));

            CreateMap<DTOs.Recipe, DAL.Entities.Recipe>()
                .ForMember(dest => dest.Instructions, opt => opt.ResolveUsing((source, destination) => string.Join(_instructionsSeparator.ToString(), source.Instructions)));
        }
    }
}
