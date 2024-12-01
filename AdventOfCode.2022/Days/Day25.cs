namespace AdventOfCode._2022.Days;

public class Day25 : BaseDay
{
    public Day25() : base("25")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var values = new List<long>();
        
        foreach (var line in lines)
        {
            values.Add(FromSnafu(line));
        }

        var valueSum = values.Sum();
        
        PrintSolution(1, ToSnafu(valueSum));
    }

    private long FromSnafu(string line)
    {
        long value = 0;
            
        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];
            var multiplier = Math.Pow(5, line.Length - 1 - i);
            var charValue = GetCharValue(c);

            value += (long)multiplier * charValue;
        }

        return value;
    }

    private string ToSnafu(long value)
    {
        var snafuValue = "";
        
        while (value > 0)
        {
            switch (value % 5)
            {
                case 0:
                {
                    snafuValue = '0' + snafuValue;
                    break;
                }
                case 1:
                {
                    snafuValue = '1' + snafuValue;
                    break;
                }
                case 2:
                {
                    snafuValue = '2' + snafuValue;
                    break;
                }
                case 3:
                {
                    value += 5;
                    snafuValue = '=' + snafuValue;
                    break;
                }
                case 4:
                {
                    value += 5;
                    snafuValue = '-' + snafuValue;
                    break;
                }
            }

            value /= 5;
        }
        
        return snafuValue;
    }

    private int GetCharValue(char c)
    {
        if (int.TryParse(c.ToString(), out var value))
        {
            return value;
        }

        return c == '-' ? -1 : -2;
    }
}