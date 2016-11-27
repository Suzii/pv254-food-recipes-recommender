namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRecommendedByField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RecommendationUsed", "RecommendedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RecommendationUsed", "RecommendedBy");
        }
    }
}
