using System.Globalization;

namespace AdventOfCode.Y2025.Days;

public class Day8 : BaseDay
{
    public Day8()
        : base("08") { }

    protected override void Solve(List<string> lines)
    {
        var points = lines.Select(line =>
        {
            var parts = line.Split(',').Select(double.Parse).ToArray();
            return new Position(parts[0], parts[1], parts[2]);
        }).ToList();
        
        Part1(points);
        Part2(points);
    }

    private static void Part1(List<Position> points)
    {
        var setOf = points.ToDictionary(p => p, p => new HashSet<Position>([p]));
        foreach (var (a, b) in GetOrderedPairs(points).Take(1000))
        {
            if (setOf[a] != setOf[b])
            {
                Connect(a, b, setOf);
            }
        }

        var solutionPart1 = setOf.Values.Distinct().OrderByDescending(x => x.Count).Take(3).Aggregate(1, (a,b) => a * b.Count);
        
        PrintSolution(1, solutionPart1.ToString());
    }
    
    private static void Part2(List<Position> points)
    {
        var componentCount = points.Count;
        var setOf = points.ToDictionary(p => p, p => new HashSet<Position>([p]));
        var solutionPart2 = 0d;

        foreach (var (a, b) in GetOrderedPairs(points).TakeWhile(_ => componentCount > 1))
        {
            if (setOf[a] != setOf[b])
            {
                Connect(a, b, setOf);
                solutionPart2 = a.X * b.X;
                componentCount--;
            }
        }

        PrintSolution(2, solutionPart2.ToString(CultureInfo.InvariantCulture));
    }
    
    private static void Connect(Position a, Position b, Dictionary<Position, HashSet<Position>> setOf)
    {
        setOf[a].UnionWith(setOf[b]);
        foreach (var p in setOf[b])
        {
            setOf[p] = setOf[a];
        }
    }
    
    private static IEnumerable<(Position, Position)> GetOrderedPairs(List<Position> points)
    {
        var pairs = 
            from a in points
            from b in points
            where (a.X, a.Y, a.Z).CompareTo((b.X, b.Y, b.Z)) < 0
            orderby a.DistanceTo(b)
            select (a, b);
        
        return pairs;
    }
    
    private sealed record Position(double X, double Y, double Z)
    {
        public double DistanceTo(Position other)
        {
            return Math.Sqrt(
                Math.Pow(other.X - X, 2) +
                Math.Pow(other.Y - Y, 2) +
                Math.Pow(other.Z - Z, 2)
            );
        }
    }
}
