﻿using System.Collections.Generic;
using System.Xml;
using RecipesParser.Models;

namespace RecipesParser.Parser
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
                PrepTime = GetPrepTime(root),
                CookTime = GetCookingTime(root),
                RecipeYield = GetPortions(root),
                Chef = GetChef(root),
                ProgrammeName = GetProgrammeName(root),
                IsVegetarian = GetIsVegeratianFlag(root),
                Ingredients = GetIngredients(root),
                Instructions = GetInstructions(root)
            };

            return result;
        }

        private static string GetImageUrl(XmlElement root)
        {
            var imageNode = root.SelectSingleNode(@"//img[@itemprop=""image""]");
            var imageUrl = imageNode?.Attributes?["src"].Value.Trim();

            return imageUrl;
        }

        private static string GetTitle(XmlElement root)
        {
            var titleNode = root.SelectSingleNode(@"//h1[@itemprop=""name""]");
            var title = titleNode?.InnerText.Trim();

            return title;
        }

        private static Time GetPrepTime(XmlElement root)
        {
            var prepTimeNode = root.SelectSingleNode(@"//p[@itemprop=""prepTime""]");
            var prepTime = new Time
            {
                FreeText = prepTimeNode?.InnerText.Trim(),
                Code = prepTimeNode?.SelectSingleNode(@"./@content")?.InnerText
            };

            return prepTime;
        }

        private static Time GetCookingTime(XmlElement root)
        {
            var cookTimeNode = root.SelectSingleNode(@"//p[@itemprop=""cookTime""]");
            var cookingTime = new Time
            {
                FreeText = cookTimeNode?.InnerText.Trim(),
                Code = cookTimeNode?.SelectSingleNode(@"./@content")?.InnerText
            };

            return cookingTime;
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

        private static Ingredient[] GetIngredients(XmlElement root)
        {
            var ingredientNodes = root.SelectNodes(@"//li[@itemprop=""ingredients""]");
            if (ingredientNodes == null)
            {
                return null;
            }

            var result = new List<Ingredient>();
            foreach (XmlNode ingredientNode in ingredientNodes)
            {
                var subCategoryNode = ingredientNode.ParentNode?.SelectSingleNode(@"./preceding-sibling::h3[@class=""recipe-ingredients__sub-heading""][1]");
                var ingredient = new Ingredient
                {
                    SubCategory = subCategoryNode?.InnerText.Trim(),
                    Name = ingredientNode.SelectSingleNode("./span")?.InnerText.Trim(),
                    FreeText = ParsingHelper.GetAllStrippedText(ingredientNode)
                };

                result.Add(ingredient);
            }

            return result.ToArray();
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
