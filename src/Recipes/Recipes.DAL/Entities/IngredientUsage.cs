namespace Recipes.DAL.Entities
{
    public class IngredientUsage
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; }

        public string SubCategory { get; set; }

        public string FreeText { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}