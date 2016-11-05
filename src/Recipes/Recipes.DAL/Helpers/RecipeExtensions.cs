using Recipes.DAL.Entities;

namespace Recipes.DAL.Helpers
{
    public static class RecipeExtensions
    {
        public static int GetTotalTimeInMinutes(this Recipe recipe)
        {
            return recipe.CookTimeInMinutes + recipe.PrepTimeInMinutes;
        }
    }
}