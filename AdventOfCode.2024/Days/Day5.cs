namespace AdventOfCode._2024.Days;

public class Day5 : BaseDay
{
    public Day5() : base("05")
    {
    }
    
    protected override void Solve(List<string> lines)
    {
        var splitIndex = lines.IndexOf("");
        var sortRules = lines.GetRange(0, splitIndex).Select(x => x.Split("|")).ToList();
        var updates = lines.GetRange(splitIndex + 1, lines.Count - splitIndex - 1).Select(x => x.Split(",")).ToList();
        
        Part1(updates, sortRules);
        Part2(updates, sortRules);
        
        // Improved
        var sorts = lines.GetRange(0, splitIndex).ToHashSet();
        updates = lines.GetRange(splitIndex + 1, lines.Count - splitIndex - 1).Select(x => x.Split(",")).ToList();
        var comparer = Comparer<string>.Create((p1, p2) => sorts.Contains($"{p1}|{p2}") ? -1 : 1);
        
        ImprovedPart1(updates, comparer);
        ImprovedPart2(updates, comparer);
    }
    
    private static void Part1(List<string[]> updates, List<string[]> sortRules)
    {
        var result = 0;
        
        foreach (var update in updates)
        {
            if (!RowIsCorrect(update, sortRules)) continue;
            result += GetMiddleNumber(update);
        }
        
        PrintSolution(1, result.ToString());
    }

    private static void Part2(List<string[]> updates, List<string[]> sortRules)
    {
        var result = 0;
        
        foreach (var update in updates)
        {
            if (RowIsCorrect(update, sortRules)) continue;
            
            // Needs improvements as this loop is done twice now
            for (int i = 0; i < update.Length - 1; i++)
            {
                var numberToCheck = update[i];
                var shouldEnter = false;
                var rules = sortRules.Where(x => x[0] == numberToCheck).ToList();
                if (rules.Count != 0)
                {
                    for (int j = i + 1; j < update.Length; j++)
                    {
                        var offsetNumber = update[j];
                        var rule = sortRules.FirstOrDefault(x => x[0] == numberToCheck && x[1] == offsetNumber);
                        if (rule == null)
                        {
                            shouldEnter = true;
                            break;
                        }
                    }
                }
                
                
                // If the current number to check is not in the first part of the rules, it's not in the correct position
                if (sortRules.All(x => x[0] != numberToCheck) || shouldEnter)
                {
                    var numbersBefore = update.ToList().GetRange(0, i);
                    var numbersAfter = update.ToList().GetRange(i + 1, update.Length - i - 1);
                    var actualRule = sortRules.First(x => x[1] == numberToCheck && !numbersBefore.Contains(x[0]) && numbersAfter.Contains(x[0]));
                    var swapIndex = Array.IndexOf(update, actualRule[0]);
                    (update[i], update[swapIndex]) = (update[swapIndex], update[i]);
                    i--; // We need to check if the swapped number is at the correct position
                }
            }    
            
            result += GetMiddleNumber(update);
        }
        
        PrintSolution(2, result.ToString());
    }

    private static void ImprovedPart1(List<string[]> updates, Comparer<string> comparer)
    {
        var result = updates.Where(update => update.SequenceEqual(update.OrderBy(x => x, comparer)))
            .Sum(GetMiddleNumber)
            .ToString();
        
        PrintSolution(1, result);
    }
    
    private static void ImprovedPart2(List<string[]> updates, Comparer<string> comparer)
    {
        var result = updates.Where(update => !update.SequenceEqual(update.OrderBy(x => x, comparer)))
            .Select(update => update.OrderBy(x => x, comparer).ToArray())
            .Sum(GetMiddleNumber)
            .ToString();
        
        PrintSolution(2, result);
    }
    
    private static bool RowIsCorrect(string[] update, List<string[]> sortRules)
    {
        var isCorrect = true;
        for (int i = 0; i < update.Length; i++)
        {
            var rules = sortRules.Where(x => x[0] == update[i]).ToList();
            for (int j = i + 1; j < update.Length; j++)
            {
                if (rules.All(x => x[1] != update[j]))
                {
                    isCorrect = false;
                    break;
                }
            }
                
        }    
        
        return isCorrect;
    }
    
    private static int GetMiddleNumber(string[] update)
    {
        var middleIndex = update.Length / 2;
        return int.Parse(update[middleIndex]);
    }
}