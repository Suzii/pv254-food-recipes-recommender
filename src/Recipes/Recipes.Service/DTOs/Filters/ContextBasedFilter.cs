using System.Collections.Generic;

namespace Recipes.Service.DTOs.Filters
{
    /// <summary>
    /// Filter to be used when suggestion should be based on current context
    /// </summary>
    public class ContextBasedFilter : PagerFilter
    {
        /// <summary>
        /// Ids of <see cref="Recipe"/> user has visited within this session
        /// </summary>
        public IList<int> RecipeIds { get; set; }
    }
}