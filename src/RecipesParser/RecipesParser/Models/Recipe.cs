namespace RecipesParser.Models
{
    public class Recipe
    {
        public string Title { get; set; }

        public Time PrepTime { get; set; }

        public Time CookingTime { get; set; }

        public string Portions { get; set; }

        public string Chef { get; set; }

        public string ProgrammeName { get; set; }

        public Ingredient[] Ingredients { get; set; }

        public string[] Instructions { get; set; }
    }
}
