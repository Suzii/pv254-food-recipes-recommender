namespace RecipesParser.Models
{
    public class Recipe
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public Time PrepTime { get; set; }

        public Time CookTime { get; set; }

        public string RecipeYield { get; set; }

        public string Chef { get; set; }

        public string ProgrammeName { get; set; }

        public bool IsVegetarian { get; set; }

        public Ingredient[] Ingredients { get; set; }

        public string[] Instructions { get; set; }
    }
}
