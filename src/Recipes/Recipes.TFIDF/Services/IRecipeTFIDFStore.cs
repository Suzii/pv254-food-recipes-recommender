using Recipes.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Services
{
    public interface IRecipeTFIDFStore
    {
        RecipeTFIDF Save(RecipeTFIDF recipeTDIDF, int TFIDFnumber = 1);
    }
}
