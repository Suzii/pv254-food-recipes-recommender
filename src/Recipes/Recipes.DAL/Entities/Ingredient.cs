using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Recipes.DAL.Entities
{
    public class Ingredient
    {
        public int Id { get; set; }

        [MaxLength(450), Required, Index("IX_Ingredient_Name", IsUnique = true)]
        public string Name { get; set; }

        public virtual List<IngredientUsage> IngredientUsages { get; set; }
    }
}