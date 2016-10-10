using System.Data.Entity;
using Recipes.DAL.Entities;

namespace Recipes.DAL
{
    public class AppContext : DbContext
    {
        public AppContext(): base("AppContext")
        {
        }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Recipe> Recipes { get; set; }

        public DbSet<IngredientUsage> IngredientUsages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<IngredientUsage>().ToTable("IngredientUsage");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
        }
    }
}
