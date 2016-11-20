namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_Ingredient_Name");
            
            CreateTable(
                "dbo.IngredientUsage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IngredientId = c.Int(nullable: false),
                        SubCategory = c.String(),
                        FreeText = c.String(),
                        RecipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ingredient", t => t.IngredientId)
                .ForeignKey("dbo.Recipe", t => t.RecipeId)
                .Index(t => t.IngredientId)
                .Index(t => t.RecipeId);
            
            CreateTable(
                "dbo.Recipe",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        ImageUrl = c.String(),
                        PrepTimeInMinutes = c.Int(nullable: false),
                        CookTimeInMinutes = c.Int(nullable: false),
                        IsVegetarian = c.Boolean(nullable: false),
                        RecipeYield = c.String(),
                        Chef = c.String(),
                        ProgrammeName = c.String(),
                        Instructions = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeTFIDF",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RecipeId = c.Int(nullable: false),
                        TFIDF = c.String(),
                        TFIDF2 = c.String(),
                        TFIDF3 = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipe", t => t.RecipeId)
                .Index(t => t.RecipeId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RecommendationUsed", "DisplayedRecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecommendationUsed", "ClickedRecipeId", "dbo.Recipe");
            DropForeignKey("dbo.RecipeTFIDF", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.IngredientUsage", "RecipeId", "dbo.Recipe");
            DropForeignKey("dbo.IngredientUsage", "IngredientId", "dbo.Ingredient");
            DropIndex("dbo.RecommendationUsed", new[] { "ClickedRecipeId" });
            DropIndex("dbo.RecommendationUsed", new[] { "DisplayedRecipeId" });
            DropIndex("dbo.RecipeTFIDF", new[] { "RecipeId" });
            DropIndex("dbo.IngredientUsage", new[] { "RecipeId" });
            DropIndex("dbo.IngredientUsage", new[] { "IngredientId" });
            DropIndex("dbo.Ingredient", "IX_Ingredient_Name");
            DropTable("dbo.RecommendationUsed");
            DropTable("dbo.RecipeTFIDF");
            DropTable("dbo.Recipe");
            DropTable("dbo.IngredientUsage");
            DropTable("dbo.Ingredient");
        }
    }
}
