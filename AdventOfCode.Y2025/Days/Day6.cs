namespace AdventOfCode.Y2025.Days;

public class Day6 : BaseDay
{
    public Day6()
        : base("06") { }

    protected override void Solve(List<string> lines)
    {
        var worksheet = ParseWorksheetV2(lines);
        var operators = lines.Last().Split(" ").Where(x => !string.IsNullOrWhiteSpace(x)).ToList();
        
        Part1(worksheet, operators);
        Part2(worksheet, operators);
    }

    private static void Part1(List<string>[] worksheet, List<string> operators)
    {
        var answerPart1 = 0L;

        for (var col = 0; col < worksheet[0].Count; col++)
        {
            var mathOperator = operators[col];
            var numbers = worksheet
                .Select(t => t[col].Trim())
                .Select(long.Parse)
                .ToList();

            var result = mathOperator switch
            {
                "+" => numbers.Sum(),
                "*" => numbers.Aggregate(1L, (a, b) => a * b),
                "-" => numbers.Aggregate((a, b) => a - b),
                _ => 0,
            };

            answerPart1 += result;
        }

        PrintSolution(1, answerPart1.ToString());
    }

    private static void Part2(List<string>[] worksheet, List<string> operators)
    {
        var answerPart2 = 0L;
        for (var col = worksheet[0].Count - 1; col >= 0; col--)
        {
            var numbers = worksheet.Select(t => t[col]).ToList();
            var mathOperation = operators[col];

            var temp = new List<long>();
            for (var i = numbers[0].Length - 1; i >= 0; i--)
            {
                var items = string.Join("", numbers.Select(t => t[i])).Trim();
                temp.Add(long.Parse(items));
            }

            var result = mathOperation switch
            {
                "+" => temp.Sum(),
                "*" => temp.Aggregate(1L, (a, b) => a * b),
                "-" => temp.Aggregate((a, b) => a - b),
                _ => 0,
            };

            answerPart2 += result;
        }

        PrintSolution(2, answerPart2.ToString());
    }

    private static List<string>[] ParseWorksheet(List<string> lines)
    {
        var rowCount = lines.Count - 1;
        var worksheet = new List<string>[rowCount];

        for (int i = 0; i < rowCount; i++)
        {
            worksheet[i] = new List<string>();
        }

        var maxLength = lines.Max(line => line.Length);
        var currentCol = 0;
        for (int i = 0; i < maxLength; i++)
        {
            // For each line, get the character at position i, if space, convert to 0
            var colChars = new List<char>();
            for (int j = 0; j < lines.Count - 1; j++)
            {
                var character = lines[j][i];
                colChars.Add(character);
            }

            // ASCII 32 is space. If colChars only contains spaces, it is a blank column and thus a separator
            if (colChars.All(x => x == 32))
            {
                currentCol++;
                continue;
            }

            for (int j = 0; j < colChars.Count; j++)
            {
                if (worksheet[j].Count == 0 || worksheet[j].Count <= currentCol)
                {
                    worksheet[j].Add(string.Empty);
                }

                worksheet[j][currentCol] += colChars[j].ToString();
            }
        }

        return worksheet;
    }
    
    private static List<string>[] ParseWorksheetV2(List<string> lines)
    {
        var rowCount = lines.Count - 1;
        var worksheet = new List<string>[rowCount];

        for (var i = 0; i < rowCount; i++)
        {
            worksheet[i] = [];
        }

        List<int> colSeparatorIndexes = [-1];
        for (var i = 0; i < lines[0].Length; i++)
        {
            var characters = lines.Select(line => line[i]).ToList();
            if (characters.All(c => c == ' '))
            {
                colSeparatorIndexes.Add(i);
            }
        }
        colSeparatorIndexes.Add(lines[0].Length);


        for (var x = 1; x < colSeparatorIndexes.Count; x++)
        {
            var startIndex = colSeparatorIndexes[x - 1] + 1;
            var length = colSeparatorIndexes[x] - startIndex;
            
            for (var i = 0; i < rowCount; i++)
            {
                var line = lines[i];
                var column = line.Substring(startIndex, length);
                worksheet[i].Add(column);
            }
        }

        return worksheet;
    }
}
