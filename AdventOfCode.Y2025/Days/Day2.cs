namespace AdventOfCode.Y2025.Days;

public class Day2 : BaseDay
{
    public Day2()
        : base("02") { }

    protected override void Solve(List<string> lines)
    {
        var line = lines.First();
        var ranges = line.Split(',');

        var answerPart1 = Part1(ranges);
        var answerPart2 = Part2(ranges);
        
        PrintSolution(1, answerPart1.ToString());
        PrintSolution(2, answerPart2.ToString());
    }

    private static long Part1(string[] ranges)
    {
        var answerPart1 = 0L;
        foreach (var range in ranges)
        {
            var bounds = range.Split('-').Select(long.Parse).ToArray();
            var start = bounds[0];
            var end = bounds[1];

            for (long i = start; i <= end; i++)
            {
                if (i.ToString().Length % 2 != 0)
                {
                    continue;
                }
                
                var firstHalf = i.ToString()[..(i.ToString().Length / 2)];
                var secondHalf = i.ToString()[(i.ToString().Length / 2)..];

                if (firstHalf == secondHalf)
                {
                    answerPart1 += i;
                }
            }
        }

        return answerPart1;
    }
    
    private static long Part2(string[] ranges)
    {
        var answerPart2 = 0L;
        foreach (var range in ranges)
        {
            var bounds = range.Split('-').Select(long.Parse).ToArray();
            var start = bounds[0];
            var end = bounds[1];

            for (long i = start; i <= end; i++)
            {
                var number = i.ToString();
                for (int j = 1; j <= number.Length / 2; j++)
                {
                    var part = number[..j];
                    var repeatedPart = string.Concat(Enumerable.Repeat(part, number.Length / part.Length));
                    if (repeatedPart == number)
                    {
                        answerPart2 += i;
                        break;
                    }
                }
            }
        }

        return answerPart2;
    }
}
