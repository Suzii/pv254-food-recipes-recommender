using System;
using System.IO;
using Newtonsoft.Json;
using RecipesParser.Parser;

namespace RecipesParser
{
    class Program
    {
        private const string DIRECTORY = "..\\..\\..\\..\\..\\data\\";

        static void Main(string[] args)
        {
            var parser = new XmlRecipeParser();
            var fileNames = Directory.GetFiles(DIRECTORY);

            foreach (var fileName in fileNames)
            {
                var recipe = parser.ParseFile(fileName);
                var serialized = JsonConvert.SerializeObject(recipe, Formatting.Indented);

                Console.WriteLine(fileName);
                Console.WriteLine(serialized);
            }

            Console.ReadLine();
        }
    }
}
