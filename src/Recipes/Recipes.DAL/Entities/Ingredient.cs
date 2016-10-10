using System.Collections.Generic;

namespace Recipes.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<IngredientUsage> IngredientUsages { get; set; }
    }
}