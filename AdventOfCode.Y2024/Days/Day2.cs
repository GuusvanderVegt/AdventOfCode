namespace AdventOfCode.Y2024.Days;

public class Day2 : BaseDay
{
    public Day2() : base("02")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var reports = lines.Select(x => x.Split(" ").Select(int.Parse).ToArray()).ToList();
        
        //Part 1
        var correctReportsCount = reports.Count(IsValid);
        
        //Part 2
        var validReports = reports.Count(x => Attenuate(x).Any(IsValid));
        
        var validReportsCount = validReports;
      
        PrintSolution(1, correctReportsCount.ToString());
        PrintSolution(2, validReportsCount.ToString());
        
    }
    
    private static IEnumerable<int[]> Attenuate(int[] report) =>
        from i in Enumerable.Range(0, report.Length + 1)
        let before = report.Take(i - 1)
        let after = report.Skip(i)
        select before.Concat(after).ToArray();
    
    private static bool IsValid(int[] report)
    {
        var pairs = report.Zip(report.Skip(1)).ToList();
        return pairs.All(p => p.First - p.Second >= 1 && p.First - p.Second <= 3) ||
               pairs.All(p => p.Second - p.First >= 1 && p.Second - p.First <= 3);
    }
}