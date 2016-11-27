namespace Recipes.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Add_RecommendationUsed_Table : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.IngredientUsage", "IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.IngredientUsage", "RecipeId", "dbo.Recipe");
            CreateTable(
                "dbo.RecommendationUsed",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayedRecipeId = c.Int(nullable: false),
                        ClickedRecipeId = c.Int(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipe", t => t.ClickedRecipeId)
                .ForeignKey("dbo.Recipe", t => t.DisplayedRecipeId)
                .Index(t => t.DisplayedRecipeId)
                .Index(t => t.ClickedRecipeId);
            
            AddForeignKey("dbo.IngredientUsage", "IngredientId", "dbo.Ingredient", "Id");
            AddForeignKey("dbo.IngredientUsage", "RecipeId", "dbo.Recipe", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IngredientUsage", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.IngredientUsage", "IngredientId", "dbo.Ingredient");
            DropForeignKey("dbo.RecommendationUsed", "DisplayedRecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecommendationUsed", "ClickedRecipeId", "dbo.Recipe");
            DropIndex("dbo.RecommendationUsed", new[] { "ClickedRecipeId" });
            DropIndex("dbo.RecommendationUsed", new[] { "DisplayedRecipeId" });
            DropTable("dbo.RecommendationUsed");
            AddForeignKey("dbo.IngredientUsage", "RecipeId", "dbo.Recipe", "Id", cascadeDelete: true);
            AddForeignKey("dbo.IngredientUsage", "IngredientId", "dbo.Ingredient", "Id", cascadeDelete: true);
        }
    }
}
