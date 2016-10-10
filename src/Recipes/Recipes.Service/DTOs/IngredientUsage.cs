namespace Recipes.Service.DTOs
{
    public class IngredientUsage
    {
        public int Id { get; set; }
        
        public Ingredient Ingredient { get; set; }

        public string SubCategory { get; set; }

        public string FreeText { get; set; }
    }
}