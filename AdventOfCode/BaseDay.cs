namespace AdventOfCode;

public abstract class BaseDay
{
    protected BaseDay(string day)
    {
        // ReSharper disable once VirtualMemberCallInConstructor
        Solve(GetLinesFromFile("Files/Day_" + day + ".txt"));
    }

    private static List<string> GetLinesFromFile(string fileName)
    {
        try
        {
            return File.ReadLines(fileName).ToList();
        } 
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return new List<string>();
        }
    }
    
    protected abstract void Solve(List<string> lines);
    
    protected static void PrintSolution(int part, string solution)
    {
        Console.WriteLine("Solution part " + part + ": " + solution);
    }
}