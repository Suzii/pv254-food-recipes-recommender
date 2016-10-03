using System;
using System.Text.RegularExpressions;
using System.Xml;
using RecipesParser.Models;

namespace RecipesParser.Parser
{
    public class ParsingHelper
    {
        public static string GetAllStrippedText(XmlNode node)
        {
            var contents = string.Empty;
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.NodeType == XmlNodeType.Element || child.NodeType == XmlNodeType.Text)
                {
                    contents += child.InnerText.Trim();
                    contents += " ";
                }
            }

            return contents.TrimEnd();
        }

        public static double ParseTimeValue(string code)
        {
            double result;
            var match = Regex.Match(code, @"(\d+)");
            var value = match.Value;
            Double.TryParse(value, out result);
            return result;
        }

        public static TimeMetric ParseTimeMetric(string code)
        {
            var match = Regex.Matches(code, @"^PT\d*(.*)$");
            var val = match[0].Groups[1].Value;
            switch (val)
            {
                case "M":
                    return TimeMetric.Minutes;
                case "H":
                    return TimeMetric.Hours;
                default:
                    throw new ArgumentOutOfRangeException(nameof(code));
            }
        }
    }
}
