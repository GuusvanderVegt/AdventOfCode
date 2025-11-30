using System.Drawing;

namespace AdventOfCode.Y2022.Days;

public class Day14 : BaseDay
{
    public Day14() : base("14")
    {
    }

    protected override void Solve(List<string> lines)
    {
        // var stoneLocations = GetStoneLocations(lines);
        var cave = GenerateCave(lines);
        var startPouringPoint = new Point(500, 0);
        
        var solutionPart1 = PourSand(cave, startPouringPoint, false);
        var solutionPart2 = PourSand(cave, startPouringPoint, true);
        
        PrintSolution(1, solutionPart1.ToString());
        PrintSolution(2, solutionPart2.ToString());
        
    }

    private int PourSand(Dictionary<Point, char> cave, Point startPoint, bool hasFloor)
    {
        var bottomRow = cave.Keys.Select(rock => rock.Y).Max();

        while (true)
        {
            var settledSandPosition = SandFlow(cave, startPoint, bottomRow);
            
            //Sand is at start position, so hole is sealed. (Part 2)
            //Sand is more likely to fall into the abyss before sealing the hole.
            if (cave.ContainsKey(settledSandPosition)) break;
            
            //Sand has fallen into the abyss, only applicable for part 1 (if cave has floor)  
            if (!hasFloor && settledSandPosition.Y >= bottomRow) break;

            cave[settledSandPosition] = 'o';
        }

        return cave.Values.Count(x => x == 'o');
    }

    private Point SandFlow(IReadOnlyDictionary<Point, char> cave, Point start, int bottomRow)
    {
        var sandPoint = start;
        while (sandPoint.Y <= bottomRow)
        {
            if (!cave.ContainsKey(sandPoint with { Y = sandPoint.Y + 1 }))
            {
                sandPoint = sandPoint with { Y = sandPoint.Y + 1 };
            }
            else if (!cave.ContainsKey(new Point(sandPoint.X - 1, sandPoint.Y + 1)))
            {
                sandPoint = new Point(sandPoint.X - 1, sandPoint.Y + 1);
            }
            else if (!cave.ContainsKey(new Point(sandPoint.X + 1, sandPoint.Y + 1)))
            {
                sandPoint = new Point(sandPoint.X + 1, sandPoint.Y + 1);
            }
            else
            {
                //Sand settles
                break;
            }
        }

        return sandPoint;
    }


    private Dictionary<Point, char> GenerateCave(List<string> input)
    {
        var cave = new Dictionary<Point, char>();
        foreach (var line in input)
        {
            var points =
                (from step in line.Split("->")
                    let parts = step.Split(",")
                    select new Point(int.Parse(parts[0]), int.Parse(parts[1]))
                ).ToArray();

            for (var i = 0; i < points.Length - 1; i++)
            {
                FillCaveWithRocks(cave, points[i], points[i + 1]);
            }
        }

        return cave;
    }

    private void FillCaveWithRocks(Dictionary<Point, char> cave, Point from, Point to)
    {
        if (from.X == to.X)
        {
            //Fill horizontal
            var fromCoordinate = Math.Min(from.Y, to.Y);
            var toCoordinate = Math.Max(from.Y, to.Y);

            for (var i = fromCoordinate; i <= toCoordinate; i++)
            {
                var point = new Point(from.X, i);
                cave[point] = '#';
            }
        }
        else if(from.Y == to.Y)
        {
            //Fill vertical
            var fromCoordinate = Math.Min(from.X, to.X);
            var toCoordinate = Math.Max(from.X, to.X);

            for (var i = fromCoordinate; i <= toCoordinate; i++)
            {
                var point = new Point(i, from.Y);
                cave[point] = '#';
            }
        }
    }
}