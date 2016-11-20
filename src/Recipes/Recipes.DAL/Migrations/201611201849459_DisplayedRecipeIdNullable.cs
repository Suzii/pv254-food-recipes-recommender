namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DisplayedRecipeIdNullable : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RecommendationUsed", new[] { "DisplayedRecipeId" });
            AlterColumn("dbo.RecommendationUsed", "DisplayedRecipeId", c => c.Int());
            CreateIndex("dbo.RecommendationUsed", "DisplayedRecipeId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RecommendationUsed", new[] { "DisplayedRecipeId" });
            AlterColumn("dbo.RecommendationUsed", "DisplayedRecipeId", c => c.Int(nullable: false));
            CreateIndex("dbo.RecommendationUsed", "DisplayedRecipeId");
        }
    }
}
