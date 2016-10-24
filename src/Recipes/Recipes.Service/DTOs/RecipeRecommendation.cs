namespace Recipes.Service.DTOs
{
    public class RecipeRecommendation
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int PrepTimeInMinutes { get; set; }

        public int CookTimeInMinutes { get; set; }

        public string Chef { get; set; }

        public string ProgrammeName { get; set; }

        public bool IsVegetarian { get; set; }

        public string ImageUrl { get; set; }
    }
}
