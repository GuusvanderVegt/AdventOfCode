using BenchmarkDotNet.Attributes;

namespace AdventOfCode._2024.Days;

public class Day4 : BaseDay
{
    public Day4() : base("04")
    {
    }
    
    [GlobalSetup]
    protected override void Solve(List<string> lines)
    {
        var wordMap = lines.Select(x => x.ToCharArray()).ToList();

        Part1(wordMap);
        Part2(wordMap);
    }
    
    [Benchmark]
    private void Part1(List<char[]> map)
    {
        var pattern = "XMAS";
        var patternLength = pattern.Length - 1;
        var enumerator = Enumerable.Range(0, pattern.Length).ToList();

        var occurences = 0;
        
        for (int y = 0; y < map.Count; y++)
        {
            var row = map[y];
            for (int x = 0; x < map[y].Length; x++)
            {
                if(row[x] != pattern[0]) continue;
                
                var leftToRightPossible = x + patternLength < row.Length;
                var rightToLeftPossible = x - patternLength >= 0;
                var topToBottomPossible = y + patternLength < map.Count;
                var bottomToTopPossible = y - patternLength >= 0;
                var topLeftToBottomRightPossible = topToBottomPossible && leftToRightPossible;
                var bottomLeftToTopRightPossible = bottomToTopPossible && leftToRightPossible;
                var topRightToBottomLeftPossible = topToBottomPossible && rightToLeftPossible;
                var bottomRightToTopLeftPossible = bottomToTopPossible && rightToLeftPossible;
                
                var leftToRight = leftToRightPossible ? string.Join("", map[y].Skip(x).Take(pattern.Length)) : string.Empty;
                var rightToLeft = rightToLeftPossible ? string.Join("", row.Skip(x - pattern.Length + 1).Take(pattern.Length).Reverse()) : string.Empty;
                var topToBottom = topToBottomPossible ? string.Join("", enumerator.Select(i => map[y + i][x])) : string.Empty;
                var bottomToTop = bottomToTopPossible ? string.Join("", enumerator.Select(i => map[y - i][x])) : string.Empty;
                var topLeftToBottomRight = topLeftToBottomRightPossible ? string.Join("", enumerator.Select(i => map[y + i][x + i])) : string.Empty;
                var bottomLeftToTopRight = bottomLeftToTopRightPossible ? string.Join("", enumerator.Select(i => map[y - i][x + i])) : string.Empty;
                var topRightToBottomLeft = topRightToBottomLeftPossible ? string.Join("", enumerator.Select(i => map[y + i][x - i])) : string.Empty;
                var bottomRightToTopLeft = bottomRightToTopLeftPossible ? string.Join("", enumerator.Select(i => map[y - i][x - i])) : string.Empty;
                
                List<string> tmp = [leftToRight, rightToLeft, topToBottom, bottomToTop, topLeftToBottomRight, bottomLeftToTopRight, topRightToBottomLeft, bottomRightToTopLeft];
                occurences += tmp.Count(s => s == pattern);
            }
        }
        
        PrintSolution(1, occurences.ToString());
    }

    private void Part2(List<char[]> map)
    {
        var occurences = 0;

        for (int y = 1; y < map.Count - 1; y++)
        {
            var row = map[y];
            for (int x = 1; x < row.Length - 1; x++)
            {
                if(row[x] != 'A') continue;
                
                var charTopLeft = map[y - 1][x - 1];
                var charTopRight = map[y - 1][x + 1];
                var charBottomLeft = map[y + 1][x - 1];
                var charBottomRight = map[y + 1][x + 1];

                var backwards = (charTopLeft == 'M' && charBottomRight == 'S') || (charTopLeft == 'S' && charBottomRight == 'M');
                var forwards = (charTopRight == 'M' && charBottomLeft == 'S') || (charTopRight == 'S' && charBottomLeft == 'M');
                
                if (backwards && forwards)
                {
                    occurences++;
                }
            }
        }
        
        PrintSolution(2, occurences.ToString());
    }
    
}