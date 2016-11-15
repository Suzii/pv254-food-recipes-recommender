namespace Recipes.Service.DTOs.Filters
{
    /// <summary>
    /// Simple filter to be used when suggestion should be based solely on recipe metadata
    /// </summary>
    public class RecipeMetadataBasedFilter : PagerFilter
    {
        /// <summary>
        /// Id of <see cref="Recipe"/>
        /// </summary>
        public int RecipeId { get; set; }

        /// <summary>
        /// TFIDF column number we use in recommendation
        /// </summary>
        public int TFIDFnumber { get; set; }
    }
}