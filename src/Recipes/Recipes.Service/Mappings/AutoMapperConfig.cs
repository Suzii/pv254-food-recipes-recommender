using AutoMapper;

namespace Recipes.Service.Mappings
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RecipeProfile>();
                cfg.AddProfile<IngredientProfile>();
                cfg.AddProfile<IngredientUsageProfile>();
            });

            return config;
        }
    }
}