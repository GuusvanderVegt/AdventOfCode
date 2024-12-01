namespace AdventOfCode._2022.Days;

public class Day10 : BaseDay
{
    public Day10() : base("10")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var x = 1;
        
        //Part 1
        var cycle = 0;
        var solutionPart1 = 0;
        var requiredCycle = 20;

        //Part 2
        var currentLine = "";

        foreach(var line in lines)
        {
            var parts = line.Split(" ");
            var command = parts[0];
            
            for (var i = 1; i <= 2; i++)
            {
                //Part 1
                cycle++;
                ExecuteRangeStrengthCalculation(cycle, ref requiredCycle, x, ref solutionPart1);
                
                //Part 2
                currentLine += GetDrawingChar(x, currentLine.Length);
                
                //Part 2
                if (currentLine.Length == 40)
                {
                    Console.WriteLine(currentLine);
                    currentLine = "";
                }
                
                if (command == "noop") break;
                
                if (i == 2)
                {
                    x += int.Parse(parts[1]);
                }
            }
        }
        
        PrintSolution(1, solutionPart1.ToString());
    }

    private string GetDrawingChar(int x, int lineLength)
    {
        if (lineLength == x - 1 || lineLength == x || lineLength == x + 1)
        {
            return "#";
        }
        
        return ".";
    }
    
    private static void ExecuteRangeStrengthCalculation(int cycle, ref int requiredCycle, int x, ref int solutionPart1)
    {
        if (cycle != requiredCycle) return;
        
        solutionPart1 += cycle * x;
        requiredCycle += 40;
    }
}