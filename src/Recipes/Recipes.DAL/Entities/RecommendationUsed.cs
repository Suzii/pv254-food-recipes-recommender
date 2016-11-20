using System;
using System.Collections.Generic;
using Recipes.DAL.Constants;

namespace Recipes.DAL.Entities
{
    public class RecommendationUsed
    {
        public int Id { get; set; }
        
        public int? DisplayedRecipeId { get; set; }

        public virtual Recipe DisplayedRecipe { get; set; }

        public int ClickedRecipeId { get; set; }

        public virtual Recipe ClickedRecipe { get; set; }
        
        /// <summary>
        /// Represents RecommenderType numbers separated by #
        /// </summary>
        public string RecommendedBy { get; set; }

        public DateTime Timestamp { get; set; }
    }
}