using System.Collections.Generic;
using System.Xml;
using Recipes.DAL.Entities;

namespace Recipes.Import.Parser
{
    public class XmlRecipeParser
    {
        public Recipe ParseFile(string fileName)
        {
            var doc = new XmlDocument();
            doc.Load(fileName);

            var root = doc.DocumentElement;
            if (root == null)
            {
                return null;
            }

            var result = new Recipe
            {
                Title = GetTitle(root),
                ImageUrl = GetImageUrl(root),
                PrepTimeInMinutes = GetPrepTime(root),
                CookTimeInMinutes = GetCookingTime(root),
                RecipeYield = GetPortions(root),
                Chef = GetChef(root),
                ProgrammeName = GetProgrammeName(root),
                IsVegetarian = GetIsVegeratianFlag(root),
                IngredientUsages = GetIngredients(root),
                Instructions = string.Join("#", GetInstructions(root))
            };

            return result;
        }

        private string GetImageUrl(XmlElement root)
        {
            // TODO
            return null;
        }

        private static string GetTitle(XmlElement root)
        {
            var titleNode = root.SelectSingleNode(@"//h1[@itemprop=""name""]");
            var title = titleNode?.InnerText.Trim();

            return title;
        }

        private static int GetPrepTime(XmlElement root)
        {
            var prepTimeNode = root.SelectSingleNode(@"//p[@itemprop=""prepTime""]");
            var code = prepTimeNode?.SelectSingleNode(@"./@content")?.InnerText;
            var prepTimeInMinutes = ParsingHelper.GetTimeInMinutes(code);

            return prepTimeInMinutes;
        }

        private static int GetCookingTime(XmlElement root)
        {
            var cookTimeNode = root.SelectSingleNode(@"//p[@itemprop=""cookTime""]");
            var code = cookTimeNode?.SelectSingleNode(@"./@content")?.InnerText;
            var cookTimeInMinutes = ParsingHelper.GetTimeInMinutes(code);

            return cookTimeInMinutes;
        }

        private static string GetPortions(XmlElement root)
        {
            var portionsNode = root.SelectSingleNode(@"//p[@itemprop=""recipeYield""]");
            var portions = portionsNode?.InnerText.Trim();

            return portions;
        }

        private static string GetChef(XmlElement root)
        {
            var chefNode = root.SelectSingleNode(@"//div[@class=""chef__name""]")?.LastChild;
            var chef = chefNode?.InnerText.Trim();

            return chef;
        }

        private static string GetProgrammeName(XmlElement root)
        {
            var programmeNode = root.SelectSingleNode(@"//div[@class=""chef__programme-name""]")?.LastChild;
            var programmeName = programmeNode?.InnerText.Trim();

            return programmeName;
        }

        private bool GetIsVegeratianFlag(XmlElement root)
        {
            var dietaryNode = root.SelectSingleNode(@"//div[@class=""recipe-metadata__dietary""]");
            var dietaryNodeContent = dietaryNode?.FirstChild?.InnerText.Trim();

            return dietaryNodeContent != null && dietaryNodeContent.Equals("Vegetarian");
        }

        private static List<IngredientUsage> GetIngredients(XmlElement root)
        {
            var ingredientNodes = root.SelectNodes(@"//li[@itemprop=""ingredients""]");
            if (ingredientNodes == null)
            {
                return null;
            }

            var result = new List<IngredientUsage>();
            foreach (XmlNode ingredientNode in ingredientNodes)
            {
                var subCategoryNode = ingredientNode.ParentNode?.SelectSingleNode(@"./preceding-sibling::h3[@class=""recipe-ingredients__sub-heading""][1]");
                var ingredient = new IngredientUsage()
                {
                    SubCategory = subCategoryNode?.InnerText.Trim(),
                    FreeText = ParsingHelper.GetAllStrippedText(ingredientNode),
                    Ingredient = new Ingredient
                    {
                        Name = ingredientNode.SelectSingleNode("./span")?.InnerText.Trim(),
                    }
                };

                result.Add(ingredient);
            }

            return result;
        }

        private static string[] GetInstructions(XmlElement root)
        {
            var instructionsNodes = root.SelectNodes(@"//li[@itemprop=""recipeInstructions""]/p");
            if (instructionsNodes == null)
            {
                return null;
            }

            var result = new List<string>();
            foreach (XmlNode instructionNode in instructionsNodes)
            {
                result.Add(instructionNode.InnerText.Trim());
            }

            return result.ToArray();
        }
    }
}
