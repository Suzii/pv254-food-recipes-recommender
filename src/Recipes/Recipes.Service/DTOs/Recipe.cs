using System.Collections.Generic;

namespace Recipes.Service.DTOs
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

        public bool IsVegetarian { get; set; }

        public string ImageUrl { get; set; }

        public List<IngredientUsage> Ingredients { get; set; }

        public List<string> Instructions { get; set; }
    }
}
