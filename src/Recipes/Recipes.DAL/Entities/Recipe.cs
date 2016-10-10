using System.Collections.Generic;

namespace Recipes.DAL.Entities
{
    public class Recipe
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PrepTimeInMinutes { get; set; }

        public int CookTimeInMinutes { get; set; }

        public string RecipeYield { get; set; }

        public string Chef { get; set; }

        public string ProgrammeName { get; set; }

        public List<IngredientUsage> IngredientUsages { get; set; }

        public string Instructions { get; set; }
    }
}
