using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.DAL.Entities
{
    public class RecipeTFIDF
    {
        public int Id { get; set; }

        public int RecipeId { get; set; }

        public string TFIDF { get; set; }

        public string TFIDF2 { get; set; }

        public string TFIDF3 { get; set; }

    }
}
