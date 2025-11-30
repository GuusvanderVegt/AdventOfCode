namespace AdventOfCode.Y2022.Days;

public class Day5 : BaseDay
{
    public Day5() : base("5")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var stacks = new List<Stack<char>>()
        {
            StringToStack("STHFWR"),
            StringToStack("SGDQW"),
            StringToStack("BTW"),
            StringToStack("DRWTNQZJ"),
            StringToStack("FDBGLVTZ"),
            StringToStack("LPTCVBSG"),
            StringToStack("ZBRTWGP"),
            StringToStack("NGMTCJR"),
            StringToStack("LGBW")
        };

        var excuteCommands = false;
        
        
        foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                excuteCommands = true;
                continue;
            }

            if (excuteCommands)
            {
                var linePart = line.Split(" ");
                
                var amount = int.Parse(linePart[1]);
                var fromStack = int.Parse(linePart[3]);
                var toStack = int.Parse(linePart[5]);
                
                var tempStack = new Stack<char>();
                
                for (var i = 0; i < amount; i++)
                {
                    //Part 1
                    // stacks[toStack - 1].Push(stacks[fromStack - 1].Pop());
                    
                    //Part 2
                    tempStack.Push(stacks[fromStack - 1].Pop());
                }
                
                //Part 2
                while (tempStack.Count > 0)
                {
                    stacks[toStack - 1].Push(tempStack.Pop());
                }
            }
        }
        
        var solution = stacks.Aggregate("", (current, stack) => current + stack.Peek());
        // PrintSolution(1, solution);
        PrintSolution(2, solution);
    }
    
    private Stack<char> StringToStack(string input)
    {
        var stack = new Stack<char>();
        foreach (var c in input)
        {
            stack.Push(c);
        }

        return stack;
    }
}