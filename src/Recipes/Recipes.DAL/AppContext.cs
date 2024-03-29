﻿using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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

        public DbSet<RecommendationUsed> RecommendationUseds { get; set; }

        public DbSet<RecipeTFIDF> RecipeTFIDFs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Recipe>().ToTable("Recipe");
            modelBuilder.Entity<IngredientUsage>().ToTable("IngredientUsage");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredient");
            modelBuilder.Entity<RecommendationUsed>().ToTable("RecommendationUsed");
            modelBuilder.Entity<RecipeTFIDF>().ToTable("RecipeTFIDF");

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
