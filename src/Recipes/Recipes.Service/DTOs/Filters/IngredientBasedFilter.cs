using System.Collections.Generic;

namespace Recipes.Service.DTOs.Filters
{
    /// <summary>
    /// Filter to be used when suggestions should be based on <see cref="Ingredient"/>
    /// </summary>
    public class IngredientBasedFilter : PagerFilter
    {
        /// <summary>
        /// Ids of <see cref="Ingredient"/>
        /// </summary>
        public IList<int> IngredientIds { get; set; }
        
        /// <summary>
        /// Upper limit on Total (preparation + cooking) time
        /// </summary>
        public int? TotalTimeTo { get; set; }
    }
}