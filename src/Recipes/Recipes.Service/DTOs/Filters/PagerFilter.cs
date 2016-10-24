namespace Recipes.Service.DTOs.Filters
{
    /// <summary>
    /// Basic filter dealing with paging of recommendations
    /// </summary>
    public abstract class PagerFilter
    {
        /// <summary>
        /// Page size defines maximum number of records to be returned
        /// </summary>
        public int? PageSize { get; set; }

        /// <summary>
        /// Page number, indexed from 1
        /// </summary>
        public int? PageNumber { get; set; }
    }
}
