using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Helpers
{
    public class DocumentStemmedHelper
    {
        public int RecipeId { get; set; }
        public List<string> StemmedDocument { get; set; }

        public DocumentStemmedHelper(int recipeid, List<string> stemmedDocument)
        {
            RecipeId = recipeid;
            StemmedDocument = stemmedDocument;
        }

    }
}
