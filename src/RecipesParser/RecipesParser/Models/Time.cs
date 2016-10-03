using System;
using System.Text.RegularExpressions;
using RecipesParser.Parser;

namespace RecipesParser.Models
{
    public class Time
    {
        public string FreeText { get; set; }

        public string Code { get; set; }

        public double ParsedTime => ParsingHelper.ParseTimeValue(Code);

        public TimeMetric ParsedMetric => ParsingHelper.ParseTimeMetric(Code);
    }
}