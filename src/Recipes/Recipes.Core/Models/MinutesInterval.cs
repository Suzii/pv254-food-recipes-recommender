namespace Recipes.Core.Models
{
    public struct MinutesInterval
    {
        public MinutesInterval(int? start, int? end)
        {
            Start = start;
            End = end;
        }

        public int? Start { get; set; }

        public int? End { get; set; }
    }
}