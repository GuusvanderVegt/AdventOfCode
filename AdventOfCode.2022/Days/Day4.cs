namespace AdventOfCode._2022.Days;

public class Day4 : BaseDay
{
    public Day4() : base("4")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var totalPart1 = 0;
        var totalPart2 = 0;
        foreach (var line in lines)
        {
            var pairs = line.Split(",");
            var numbersPart1 = pairs[0].Split("-").Select(int.Parse).ToList();
            var numbersPart2 = pairs[1].Split("-").Select(int.Parse).ToList();
            
            //Part 1
            if ((numbersPart1[0] <= numbersPart2[0] && numbersPart1[1] >= numbersPart2[1]) ||
                (numbersPart1[0] >= numbersPart2[0] && numbersPart1[1] <= numbersPart2[1]))
            {
                totalPart1++;
            }
            
            //Part 2
            if((numbersPart1[0] <= numbersPart2[0] && numbersPart1[1] >= numbersPart2[0]) ||
               (numbersPart1[0] >= numbersPart2[0] && numbersPart1[0] <= numbersPart2[1]))
            {
                totalPart2 ++;
            }
            
        }
        
        PrintSolution(1, totalPart1.ToString());
        PrintSolution(1, totalPart2.ToString());
    }
}