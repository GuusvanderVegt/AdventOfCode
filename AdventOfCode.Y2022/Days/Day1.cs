namespace AdventOfCode.Y2022.Days;

public class Day1 : BaseDay
{
    public Day1() : base("1")
    {
    }

    protected override void Solve(List<string> lines)
    {
        
        //Part 1
        var totalCalories = 0;
        var caloriesList = new List<int>();

        for (var i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            
            //If line is empty, or last line in file. Save calories and reset totalCalories
            if (line == "" || i == lines.Count - 1)
            {
                caloriesList.Add(totalCalories);
                totalCalories = 0;
                continue;
            }
            
            var calories = int.Parse(line);
            totalCalories += calories;
        }
        
        var highestCalories = caloriesList.Max();
        PrintSolution(1, highestCalories.ToString());
        
        //Part 2
        var topThreeTotalCalories = caloriesList.OrderByDescending(x => x).Take(3).Sum();
        PrintSolution(2, topThreeTotalCalories.ToString());
    }
}