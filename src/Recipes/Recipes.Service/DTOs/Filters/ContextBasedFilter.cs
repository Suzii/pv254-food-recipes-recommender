namespace Recipes.Service.DTOs.Filters
{
    /// <summary>
    /// Filter to be used when suggestion should be based on current context
    /// </summary>
    public class ContextBasedFilter : PagerFilter
    {
        /// <summary>
        /// Id of <see cref="Recipe"/>
        /// </summary>
        public int RecipeId { get; set; }

        // TODO some data about current user or his session or whatever
    }
}