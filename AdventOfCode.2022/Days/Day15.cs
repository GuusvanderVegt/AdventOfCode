using System.Drawing;
using System.Text.RegularExpressions;

namespace AdventOfCode._2022.Days;

public class Day15 : BaseDay
{
    public Day15() : base("15")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var solutionPart1 = 0;
        var solutionPart2 = 0L;
        
        const int yPoint = 2000000;
        var pairs = GetItemsFromInput(lines).ToArray();
        
        var rects = pairs.Select(x => x.ToRect()).ToArray();
        var left = rects.Select(x => x.Left).Min();
        var right = rects.Select(x => x.Right).Max();


        for (int i = left; i <= right; i++)
        {
            var point = new Point(i, yPoint);
            if (pairs.Any(x => x.beacon != point && x.InRange(point)))
            {
                solutionPart1++;
            }
        }
        
        var area = GetUncoveredAreas(pairs, new Rect(0, 0, 4000001, 4000001)).First();
        solutionPart2 = area.X * 4000000L + area.Y;
    
        PrintSolution(1, solutionPart1.ToString());
        PrintSolution(2, solutionPart2.ToString());
    }
    
    private IEnumerable<Rect> GetUncoveredAreas(Pair[] pairing, Rect rect)
    {
        if (rect.Width == 0 || rect.Height == 0)
            yield break;

        foreach (var pair in pairing)
        {
            if (rect.Corners.All(corner => pair.InRange(corner)))
                yield break;
        }

        if (rect.Width == 1 && rect.Height == 1)
        {
            yield return rect;
            yield break;
        }

        foreach (var rectT in rect.Split())
        {
            foreach (var area in GetUncoveredAreas(pairing, rectT))
            {
                yield return area;
            }
        }
    }
    
    private IEnumerable<Pair> GetItemsFromInput(List<string> input)
    {
        foreach (var line in input)
        {
            var positions = Regex.Matches(line, "-?[0-9]+").Select(m => int.Parse(m.Value)).ToArray();
            yield return new Pair(
                sensor: new Point(positions[0], positions[1]),
                beacon: new Point(positions[2], positions[3])
            );
        }
    }
    private record struct Pair(Point sensor, Point beacon)
    {
        public int Radius = Manhattan(sensor, beacon);
        
        private static int Manhattan(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        public bool InRange(Point point) => Manhattan(point, sensor) <= Radius;

        public Rect ToRect()
        {
            return new Rect(sensor.X - Radius, sensor.Y - Radius, 2 * Radius + 1, 2 * Radius + 1);
        }
    }

    private record struct Rect(int X, int Y, int Width, int Height)
    {
        public int Left => X;
        public int Right => X + Width - 1;
        public int Top => Y;
        public int Bottom => Y + Height - 1;
        
        public IEnumerable<Point> Corners
        {
            get
            {
                yield return new Point(Left, Top);
                yield return new Point(Right, Top);
                yield return new Point(Right, Bottom);
                yield return new Point(Left, Bottom);
            }
        }
        
        public IEnumerable<Rect> Split()
        {
            var w0 = Width / 2;
            var w1 = Width - w0;
            var h0 = Height / 2;
            var h1 = Height - h0;
            yield return new Rect(Left, Top, w0, h0);
            yield return new Rect(Left + w0, Top, w1, h0);
            yield return new Rect(Left, Top + h0, w0, h1);
            yield return new Rect(Left + w0, Top + h0, w1, h1);
        }


    }
}