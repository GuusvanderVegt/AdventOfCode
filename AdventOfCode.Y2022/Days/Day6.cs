namespace AdventOfCode.Y2022.Days;

public class Day6 : BaseDay
{
    public Day6() : base("6")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var line = lines.First();
        var solutionPart1 = Solve(4, line);
        var solutionPart2 = Solve(14, line);

        PrintSolution(1, solutionPart1.ToString());
        PrintSolution(2, solutionPart2.ToString());
    }

    private int Solve(int uniqueSequenceLength, string line)
    {
        var solution = 0;

        for (var i = uniqueSequenceLength - 1; i < line.Length; i++)
        {
            var charSequence = line.Substring(i - (uniqueSequenceLength - 1), uniqueSequenceLength);
            
            if (charSequence.Distinct().Count() != uniqueSequenceLength) continue;
            
            solution = i + 1;
            break;
        }

        return solution;
    }
}