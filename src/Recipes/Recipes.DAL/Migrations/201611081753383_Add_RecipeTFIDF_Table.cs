namespace Recipes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_RecipeTFIDF_Table : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RecipeTFIDF");
        }
    }
}
