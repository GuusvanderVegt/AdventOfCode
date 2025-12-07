namespace AdventOfCode.Y2025.Days;

public class Day7 : BaseDay
{
    public Day7()
        : base("07") { }

    protected override void Solve(List<string> lines)
    {
        var diagram = new char[lines.Count][];
        for (var i = 0; i < lines.Count; i++)
        {
            var length = lines[i].Length;
            diagram[i] = new char[length];
            for (var j = 0; j < length; j++)
            {
                diagram[i][j] = lines[i][j];
            }
        }
        
        var startY = lines[0].IndexOf('S');
        List<int> beamYIndexes = [startY];
        
        // Start at 1 as we can skip the first row
        for (var i = 1; i < lines.Count; i++)
        {
            var line = diagram[i];
            var splitIndices = line.Select((c, index) => new { c, index })
                .Where(x => x.c == '^')
                .Select(x => x.index)
                .ToList();

            if (splitIndices.Count == 0)
            {
                foreach (var beamIndice in beamYIndexes)
                {
                    line[beamIndice] = '|';
                }
            }
        }
    }

}
