using System.Drawing;

namespace AdventOfCode.Y2022.Days;

public class Day12 : BaseDay
{
    private readonly Dictionary<Point, int> _grid = new();
    private int _width, _height;
    private Point _start, _end;
    
    public Day12() : base("12")
    {
    }

    protected override void Solve(List<string> lines)
    {
        _width = lines.First().Length;
        _height = lines.Count;
        
        InitializeGrid(lines);

        var solutionPart1 = FloodFill(true);
        var solutionPart2 = FloodFill(false);
        
        PrintSolution(1, solutionPart1.ToString());
        PrintSolution(2, solutionPart2.ToString());
    }

    private int FloodFill(bool part1)
    {
        //Shortest path indicates the points of the path, where the int is the amount of steps to the point from the start.
        var shortestPath = new Dictionary<Point, int>();
        var queue = new Queue<Point>();

        shortestPath.Add(_start, 0);
        queue.Enqueue(_start);

        while (queue.Count > 0)
        {
            var point = queue.Dequeue();

            if (point.X != 0) CheckPoint(point, point with { X = point.X - 1 }, queue, shortestPath, part1);
            if (point.X != _height - 1) CheckPoint(point, point with { X = point.X + 1 }, queue, shortestPath, part1);
            if (point.Y != 0) CheckPoint(point, point with { Y = point.Y - 1 }, queue, shortestPath, part1);
            if (point.Y != _width - 1) CheckPoint(point, point with { Y = point.Y + 1 }, queue, shortestPath, part1);
        }

        return shortestPath[_end];
    }

    private void CheckPoint(Point p, Point dirPoint, Queue<Point> queue, Dictionary<Point, int> shortestPath, bool part1)
    {
        var destHeight = _grid[dirPoint];
        var currentPointHeight = _grid[p];

        if (destHeight - currentPointHeight <= 1)
        {
            var pathLength = shortestPath[p] + 1;
            if (shortestPath.GetValueOrDefault(dirPoint, int.MaxValue) > pathLength)
            {
                queue.Enqueue(dirPoint);
                shortestPath[dirPoint] = !part1 && destHeight == 0 ? 0 : pathLength;
            }
        }
    }

    private void InitializeGrid(IReadOnlyList<string> lines)
    {
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            for (var j = 0; j < line.Length; j++)
            {
                var c = line[j];
                var point = new Point(i, j);

                if (c == 'S')
                {
                    _start = point;
                    c = 'a';
                }

                if (c == 'E')
                {
                    _end = point;
                    c = 'z';
                }
                
                _grid.Add(point, c - 'a');
            }
        }
    }
}