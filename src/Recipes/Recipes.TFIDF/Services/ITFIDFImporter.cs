﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.TFIDF.Services
{
    public interface ITFIDFImporter
    {
        Task ImportTFIDF(int titlesRepeat, int importValuesCount, int TFIDFnumber = 1);
    }
}
