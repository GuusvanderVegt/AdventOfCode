namespace AdventOfCode.Y2025.Days;

public class Day7 : BaseDay
{
    public Day7()
        : base("07") { }

    protected override void Solve(List<string> lines)
    {
        Part1(lines);
        Part2(lines);
    }

    private static void Part1(List<string> lines)
    {
        var startY = lines[0].IndexOf('S');
        HashSet<int> beamYIndexes = [startY];
        var answerPart1 = 0;
        
        // Start at 1 as we can skip the first row
        for (var i = 1; i < lines.Count; i++)
        {
            var line = lines[i];
            var splitIndices = line.Select((c, index) => new { c, index })
                .Where(x => x.c == '^')
                .Select(x => x.index)
                .Where(index => beamYIndexes.Contains(index))
                .ToList();

            foreach (var splitIndex in splitIndices)
            {
                answerPart1++;

                var prev = splitIndex - 1;
                var next = splitIndex + 1;
                if(prev > 0)
                {
                    beamYIndexes.Add(prev);
                }

                if (next < line.Length)
                {
                    beamYIndexes.Add(next);
                }
                
                beamYIndexes.Remove(splitIndex);
            }
        }
        
        PrintSolution(1, answerPart1.ToString());
    }
    
    private static void Part2(List<string> lines)
    {
        var startY = lines[0].IndexOf('S');
        Dictionary<int, long> pathsPerPosition = new() { [startY] = 1 };
    
        for (var i = 1; i < lines.Count; i++)
        {
            var line = lines[i];
            Dictionary<int, long> newPathsPerPosition = new();
    
            foreach (var (position, pathCount) in pathsPerPosition)
            {
                if (line[position] == '^')
                {
                    // Split: distribute paths to left and right
                    var left = position - 1;
                    var right = position + 1;
    
                    if (left >= 0)
                    {
                        newPathsPerPosition[left] = newPathsPerPosition.GetValueOrDefault(left) + pathCount;
                    }
    
                    if (right < line.Length)
                    {
                        newPathsPerPosition[right] = newPathsPerPosition.GetValueOrDefault(right) + pathCount;
                    }
                }
                else
                {
                    // No split: carry forward all paths to same position
                    newPathsPerPosition[position] = newPathsPerPosition.GetValueOrDefault(position) + pathCount;
                }
            }
    
            pathsPerPosition = newPathsPerPosition;
        }
    
        var answerPart2 = pathsPerPosition.Values.Sum();
        PrintSolution(2, answerPart2.ToString());
    }
}
