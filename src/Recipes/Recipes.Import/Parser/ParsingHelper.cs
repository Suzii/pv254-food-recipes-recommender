using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace Recipes.Import.Parser
{
    internal class ParsingHelper
    {
        private const int SecondsPerMinute = 60;

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

        public static int GetTimeInMinutes(string code)
        {
            if (String.IsNullOrEmpty(code))
                return 0;

            var metric = ParseTimeMetric(code);
            var time = ParseTimeValue(code);

            return (metric == TimeMetric.Minutes) ? time : time * SecondsPerMinute;
        }

        internal static int ParseTimeValue(string code)
        {
            int result;
            var match = Regex.Match(code, @"(\d+)");
            var value = match.Value;
            int.TryParse(value, out result);
            return result;
        }

        internal static TimeMetric ParseTimeMetric(string code)
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
