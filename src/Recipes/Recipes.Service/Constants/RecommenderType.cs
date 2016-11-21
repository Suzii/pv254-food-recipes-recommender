namespace Recipes.Service.Constants
{
    /// <summary>
    /// Represents type of recommender that was used.
    /// This class has equivalent representation in DAL, DTO and in JavaScript as well 
    /// so make sure to update all of them if needed
    /// </summary>
    public enum RecommenderType
    {
        IngredientBased = 0,
        TfIdf = 1,
        UserContext = 2,
        RecipeSearch = 3,
        IngredientSearch = 4,
        MostPopularList = 5,
    }
}