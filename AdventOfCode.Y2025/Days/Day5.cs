namespace AdventOfCode.Y2025.Days;

public class Day5 : BaseDay
{
    public Day5()
        : base("05") { }

    protected override void Solve(List<string> lines)
    {
        var ranges = GetRanges(lines);
        var ingredientIds = GetIngredientIds(lines);
        var freshIngredients = 0;

        foreach (var ingredientId in ingredientIds)
        {
            if (IsInAnyRange(ingredientId, ranges))
            {
                freshIngredients++;
            }
        }

        PrintSolution(1, freshIngredients.ToString());
        PrintSolution(2, GetAllRangesCounts(ranges).ToString());
    }

    private static long GetAllRangesCounts(List<(long Start, long End)> ranges)
    {
        // Sort intervals by start
        ranges.Sort((a, b) => a.Start.CompareTo(b.Start));

        // Merge intervals
        var mergedIntervals = new List<(long Start, long End)>();
        var currentInterval = ranges.First();

        for (var i = 1; i < ranges.Count; i++)
        {
            if (ranges[i].Start <= currentInterval.End + 1)
            {
                // Overlapping or contiguous intervals, merge them
                currentInterval = (
                    currentInterval.Start,
                    Math.Max(currentInterval.End, ranges[i].End)
                );
            }
            else
            {
                // No overlap, add the current interval to merged list
                mergedIntervals.Add(currentInterval);
                currentInterval = ranges[i];
            }
        }

        mergedIntervals.Add(currentInterval);
        return mergedIntervals.Sum(interval => interval.End - interval.Start + 1);
    }

    private static List<(long Start, long End)> GetRanges(List<string> lines)
    {
        var endIndex = lines.IndexOf(string.Empty);
        return lines
            .GetRange(0, endIndex)
            .Select(x =>
            {
                var parts = x.Split('-');
                return (long.Parse(parts[0]), long.Parse(parts[1]));
            })
            .ToList();
    }

    private static List<long> GetIngredientIds(List<string> lines)
    {
        var ingredientIds = new List<long>();
        var startIndex = lines.IndexOf(string.Empty) + 1;
        for (var i = startIndex; i < lines.Count; i++)
        {
            ingredientIds.Add(long.Parse(lines[i]));
        }

        return ingredientIds;
    }

    private static bool IsInAnyRange(long ingredientId, List<(long Start, long End)> ranges)
        => ranges.Any(range => ingredientId >= range.Start && ingredientId <= range.End);
}
