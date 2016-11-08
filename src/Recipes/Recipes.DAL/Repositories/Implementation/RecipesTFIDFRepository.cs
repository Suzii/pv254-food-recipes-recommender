using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Recipes.DAL.Entities;
using Recipes.DAL.Helpers;
using System.Data.Entity.SqlServer;
using System;

namespace Recipes.DAL.Repositories.Implementation
{
    public class RecipesTFIDFRepository : IRecipesTFIDFRepository
    {
        public async Task<RecipeTFIDF> GetRecipeTFIDFAsync(int id)
        {
            using (var dbContext = new AppContext())
            {
                var result = await dbContext.RecipeTFIDFs.
                                SingleOrDefaultAsync<RecipeTFIDF>(r => r.RecipeId == id);

                return result;
            }
        }

        public RecipeTFIDF Save(RecipeTFIDF recipeTFIDF, int TFIDFnumber = 1)
        {
            using (var dbContext = new AppContext())
            {
                var recipe = GetRecipeTFIDFAsync(recipeTFIDF.RecipeId).Result;
                RecipeTFIDF result;
                if (recipe == null)
                {
                    result = dbContext.RecipeTFIDFs.Add(recipeTFIDF);
                }
                else
                {
                    switch (TFIDFnumber)
                    {
                        case 1:
                            recipe.TFIDF = recipeTFIDF.TFIDF;
                            break;
                        case 2:
                            recipe.TFIDF2 = recipeTFIDF.TFIDF2;
                            break;
                        case 3:
                            recipe.TFIDF3 = recipeTFIDF.TFIDF3;
                            break;
                    }

                    dbContext.Entry(recipe).State = EntityState.Modified;
                    result = recipe;
                }
                dbContext.SaveChanges();
                return result;

            }
        }
    }
}
