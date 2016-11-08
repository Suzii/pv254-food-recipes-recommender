using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Helpers
{
    public class DocumentVectorsHelper
    {
        public int RecipeId { get; set; }
        public List<double> DocumentVector { get; set; }

        public DocumentVectorsHelper(int recipeid, List<double> documentVector)
        {
            RecipeId = recipeid;
            DocumentVector = documentVector;
        }
    }
}
