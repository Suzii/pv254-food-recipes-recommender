using System;
using System.Collections.Generic;
using Recipes.Service.Constants;

namespace Recipes.Service.DTOs.UserActivity
{
    public class RecommendationUsed
    {
        public int Id { get; set; }

        public int DisplayedRecipeId { get; set; }

        public int ClickedRecipeId { get; set; }

        public IEnumerable<RecommenderType> RecommendedBy { get; set; }

        public DateTime Timestamp { get; set; }
    }
}