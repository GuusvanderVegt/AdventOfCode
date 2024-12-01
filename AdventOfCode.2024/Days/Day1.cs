namespace AdventOfCode._2024.Days;

public class Day1 : BaseDay
{
    public Day1() : base("01")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var leftList = lines.Select(x => x.Split("   ")[0]).Select(int.Parse).OrderDescending().ToList();
        var rightList = lines.Select(x => x.Split("   ")[1]).Select(int.Parse).OrderDescending().ToList();
        
        var answerPart1 = leftList.Zip(rightList, (l, r) => Math.Abs(l - r)).Sum();
        var answerPart2 = leftList.Select(x => rightList.Count(y => y == x) * x).Sum();
        
        PrintSolution(1, answerPart1.ToString());
        PrintSolution(2, answerPart2.ToString());
    }
}