namespace Recipes.DAL.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddIngredientNameIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Ingredient", "Name", unique: true, name: "IX_Ingredient_Name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ingredient", "IX_Ingredient_Name");
        }
    }
}
