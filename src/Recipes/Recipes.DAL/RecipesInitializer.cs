using System.Collections.Generic;
using Recipes.DAL.Entities;

namespace Recipes.DAL
{
    class RecipesInitializer : System.Data.Entity.CreateDatabaseIfNotExists<AppContext>
    {
        protected override void Seed(AppContext context)
        {
            /*var recipes = new List<Recipe> { GetPizzaRecipe() };

            recipes.ForEach(r => context.Recipes.Add(r));
            context.SaveChanges();*/
        }

        private static Recipe GetPizzaRecipe()
        {
            return new Recipe()
            {

                Title = "Ten-minute pizza",
                PrepTimeInMinutes = 30,
                CookTimeInMinutes = 30,
                RecipeYield = "Makes 1",
                Chef = "James Tanner",
                ProgrammeName = "Ready Steady Cook",
                IngredientUsages = new List<IngredientUsage>()
                {
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "plain flour" },
                        SubCategory = "For the pizza base",
                        FreeText = "250g/83oz plain flour , plus extra for dusting"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "olive oil"},
                        SubCategory = "For the pizza base",
                        FreeText = "8 tbsp olive oil"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "salt"},
                        SubCategory = "For the pizza base",
                        FreeText = "1 tsp sea salt"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "tomato purée"},
                        SubCategory = "For the topping",
                        FreeText = "4 tbsp tomato purée"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "shiitake mushrooms"},
                        SubCategory = "For the topping",
                        FreeText = "60g/21oz shiitake mushrooms , sliced"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "prosciutto"},
                        SubCategory = "For the topping",
                        FreeText = "4 slices prosciutto"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "gorgonzola"},
                        SubCategory = "For the topping",
                        FreeText = "100g/31oz gorgonzola cheese"
                    },
                    new IngredientUsage()
                    {
                        Ingredient = new Ingredient() { Name = "egg"},
                        SubCategory = "For the topping",
                        FreeText = "1 free-range egg"
                    }
                },
                Instructions = string.Join("#", "Preheat the oven to 200C/400F/Gas 6.", "For the pizza base, place the flour, oil, water and salt into a food processor and blend together until a dough is formed. Tip out onto a floured work surface and knead. Shape into a round base about 20cm/8in wide.", "Place into a frying pan over a high heat and brown the base, then using a mini-blowtorch, crisp the top of the pizza. (Alternatively you can do this under the grill.)", "For the topping, spread tomato purée over the top of the base.", "Fry the mushrooms in a dry frying pan then scatter over the tomato purée. Arrange the prosciutto and cheese on top.", "Crack an egg into the middle, then place into the oven for five minutes to finish cooking.", "Serve on a large plate, and slice into wedges to serve.")
            };
        }
    }
}