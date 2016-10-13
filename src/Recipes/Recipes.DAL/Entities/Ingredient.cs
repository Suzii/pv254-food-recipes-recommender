using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Recipes.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<IngredientUsage> IngredientUsages { get; set; }
    }
}