using System.ComponentModel.DataAnnotations;

namespace AdventOfCode.Y2025.Days;

public class Day3 : BaseDay
{
    public Day3()
        : base("03") { }

    protected override void Solve(List<string> lines)
    {
        Part1(lines);
        Part2(lines);
    }

    private void Part1(List<string> lines)
    {
        var answerPart1 = 0;
        foreach (var bank in lines)
        {
            var batteries = bank.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            
            var firstMax = batteries.Max();
            var firstMaxIndex = Array.IndexOf(batteries, firstMax);

            string joltage;
            if (firstMaxIndex == batteries.Length - 1)
            {
                // If the first max is the last element, take the second max from the start to the first max
                var remainingBatteries = batteries.Take(firstMaxIndex).ToArray();
                var secondMax = remainingBatteries.Max();
                joltage = string.Concat([secondMax.ToString(), firstMax.ToString()]);
            }
            else
            {
                var remainingBatteries = batteries.Skip(firstMaxIndex + 1).ToArray();
                var secondMax = remainingBatteries.Max();
                joltage = string.Concat([firstMax.ToString(), secondMax.ToString()]);
            }
            
            answerPart1 += int.Parse(joltage);
        }
        
        PrintSolution(1, answerPart1.ToString());
    }
    
    private static void Part2(List<string> lines)
    {
        var desiredLength = 12;
        var answerPart2 = 0L;

        foreach (var bank in lines)
        {
            var joltage = string.Empty;
            var batteries = bank.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
            while(joltage.Length < desiredLength)
            {
                var endBatteriesToSkip = desiredLength - joltage.Length - 1;
                var startBatteriesToTake = batteries.Length - endBatteriesToSkip;
                var currentBatteries = batteries.Take(startBatteriesToTake).ToArray();
                var currentMax = currentBatteries.Max();
                joltage += currentMax.ToString();
                
                batteries = batteries.Skip(Array.IndexOf(batteries, currentMax) + 1).ToArray();
            }
            
            answerPart2 += long.Parse(joltage);
        }
        
        PrintSolution(2, answerPart2.ToString());
    }
}
