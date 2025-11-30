using System.Text.RegularExpressions;

namespace AdventOfCode.Y2024.Days;

public class Day3 : BaseDay
{
    public Day3() : base("03")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var map = string.Join("", lines);
        
        // Part 1
        var mulOperations = Regex.Matches(map, @"mul\((\d{1,3}),(\d{1,3})\)").Select(x => x.ToString());
        var answerPart1 = (
            from operation in mulOperations 
            select operation.Substring(4, operation.Length - 5) into substring 
            select substring.Split(',').Select(int.Parse).ToArray() into numbers 
            select numbers[0] * numbers[1]
        ).Sum();

        PrintSolution(1, answerPart1.ToString());
        
        // Part 2
        var operations = Regex.Matches(map, @"mul\((\d+),(\d+)\)|don't\(\)|do\(\)");
        var allowExecution = true;
        var result = 0;
        foreach (Match operation in operations)
        {
            switch (operation.Value)
            {
                case "don't()":
                    allowExecution = false;
                    continue;
                case "do()":
                    allowExecution = true;
                    continue;
            }

            if (!allowExecution) continue;
            result += int.Parse(operation.Groups[1].Value) * int.Parse(operation.Groups[2].Value);
        }
        
        PrintSolution(2, result.ToString());
    }
}