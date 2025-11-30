namespace AdventOfCode.Y2022.Days;

public class Day11 : BaseDay
{
    public Day11() : base("11")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var monkeys = new List<Monkey>();
        var nrOfRounds = 10000;
        var nrOfMonkeys = lines.Count(x => x.Contains("Monkey"));
        var nrOfItemsProcessedPerMonkey = new long[nrOfMonkeys];

        for (var j = 0; j < lines.Count; j+= 7)
        {
            var monkeyLines = lines.GetRange(j, 6);
            var monkeyNr = int.Parse(monkeyLines[0].Split(" ")[1][0].ToString());
            var levels = monkeyLines[1].Split(": ")[1].Split(", ").Select(x => long.Parse(x)).ToList();
            var trueMonkey = int.Parse(monkeyLines[4].Split(" ").Last());
            var falseMonkey = int.Parse(monkeyLines[5].Split(" ").Last());
            var divisibleValue = int.Parse(monkeyLines[3].Split(" ").Last());
        
            var monkey = new Monkey(monkeyNr, levels, trueMonkey, falseMonkey, divisibleValue, monkeyLines[2]);
            monkeys.Add(monkey);
        }

        var testValue = monkeys.Aggregate(1, (current, monkey) => current * monkey.DivisibleValue);

        for (int i = 0; i < nrOfRounds; i++)
        {
            foreach(var monkey in monkeys)
            {
                while(monkey.Levels.Count > 0)
                {

                    var level = monkey.Levels.First();
                    var newLevel = CalculateNewLevel(level, monkey.calculationOperation) % testValue;
                    if(newLevel % monkey.DivisibleValue == 0)
                    {
                        monkeys[monkey.TrueMonkey].Levels.Add(newLevel);
                    }
                    else
                    {
                        monkeys[monkey.FalseMonkey].Levels.Add(newLevel);
                    }

                    monkey.Levels.Remove(level);
                    nrOfItemsProcessedPerMonkey[monkey.MonkeyNr] ++;
                }
            }
        }

        var ordered = nrOfItemsProcessedPerMonkey.OrderByDescending(x => x).ToList();
        var result = ordered[0] * ordered[1];
        PrintSolution(1, result.ToString());
    }

    private long CalculateNewLevel(long level, string monkeyLine)
    {
        var calculation = monkeyLine.Split("= ")[1];
        var calcParts = calculation.Split(" ");
        var type = calcParts[1];

        var valueToUse = calcParts.Last() == "old" ? level : int.Parse(calcParts.Last());

        if (type == "+") return level + valueToUse;
        if (type == "-") return level - valueToUse;
        if (type == "*") return level * valueToUse;
        if (type == "/") return level / valueToUse;

        throw new Exception("Unknown type");
    }
}

class Monkey
{
    public int MonkeyNr { get; set; }
    public List<long> Levels { get; set; }
    public int TrueMonkey { get; set; }
    public int FalseMonkey { get; set; }
    public int DivisibleValue { get; set; }
    public string calculationOperation { get; set; }
    
    public Monkey(int monkeyNr, List<long> levels, int trueMonkey, int falseMonkey, int divisibleValue, string calculationOperation)
    {
        MonkeyNr = monkeyNr;
        Levels = levels;
        TrueMonkey = trueMonkey;
        FalseMonkey = falseMonkey;
        DivisibleValue = divisibleValue;
        this.calculationOperation = calculationOperation;
    }
}