using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipesParser.Models;
using RecipesParser.Parser;

namespace Tests
{
    [TestClass]
    public class ParsingHelperTest
    {
        [TestMethod]
        public void ParseTimeValue_ValidInput()
        {
            var input = "PT10M";
            var expected = 10.0;
            var actual = ParsingHelper.ParseTimeValue(input);

            Assert.AreEqual(expected, actual, "Values do not match");
        }

        [TestMethod]
        public void ParseTimeMetric_ValidMinutesInput()
        {
            var input = "PT10M";
            var expected = TimeMetric.Minutes;
            var actual = ParsingHelper.ParseTimeMetric(input);

            Assert.AreEqual(expected, actual, "Values do not match");
        }

        [TestMethod]
        public void ParseTimeMetric_ValidHoursInput()
        {
            var input = "PT1H";
            var expected = TimeMetric.Hours;
            var actual = ParsingHelper.ParseTimeMetric(input);

            Assert.AreEqual(expected, actual, "Values do not match");
        }
    }
}
