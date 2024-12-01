namespace AdventOfCode._2022.Days;

public class Day3 : BaseDay
{
    public Day3() : base("3")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var totalValuePart1 = 0;
        var totalValuePart2 = 0;
        
        //Part 1
        foreach (var line in lines)
        {
            var firstHalf = line[..(line.Length / 2)];
            var secondHalf = line[(line.Length / 2)..];

            var c = firstHalf.Intersect(secondHalf).First();
            
            if(c < 97) totalValuePart1 += c - 38;
            else totalValuePart1 += c - 96;
        }
        
        //Part 2
        for (var i = 0; i < lines.Count; i+= 3)
        {
            var firstLine = lines[i];
            var secondLine = lines[i + 1];
            var thirdLine = lines[i + 2];

            var c = firstLine.Intersect(secondLine).Intersect(thirdLine).First();
            
            if(c < 97) totalValuePart2 += c - 38;
            else totalValuePart2 += c - 96;
        }
        
        PrintSolution(1, totalValuePart1.ToString());
        PrintSolution(2, totalValuePart2.ToString());
    }
}