namespace AdventOfCode._2022.Days;

public class Day9Thijs : BaseDay
{
    private List<(string, int)> _input = default!;
    
    public Day9Thijs() : base("9")
    {
    }

    protected override void Solve(List<string> lines)
    {
        _input = lines.Select(l => l.Split(' ')).Select(l => (l[0], int.Parse(l[1]))).ToList();
        
        PrintSolution(1, GoThroughMovements(2).ToString());
        PrintSolution(2, GoThroughMovements(10).ToString());
    }
    
    private int GoThroughMovements(int ropeLenght)
    {
        HashSet<(int X, int Y)> visitedPositions = new();
        List<(int x, int y)> knots = Enumerable.Repeat((0, 0), ropeLenght).ToList();

        foreach (var (direction, steps) in _input)
        {
            for (int i = 0; i < steps; i++)
            {
                (int x, int y) head = knots[0];
                MoveHead(ref head, direction);
                knots[0] = head;

                for (int j = 1; j < knots.Count; j++)
                {
                    var knot = knots[j];
                    MoveKnot(ref knot, knots[j - 1]);
                    knots[j] = knot;
                }

                visitedPositions.Add((knots[^1].x, knots[^1].y));
            }
        }

        return visitedPositions.Count;
    }
    
    private static void MoveHead(ref (int x, int y) head, string direction)
    {
        if (direction == "L") head.x -= 1;
        if (direction == "R") head.x += 1;
        if (direction == "U") head.y += 1;
        if (direction == "D") head.y -= 1;
    }
    
    private static void MoveKnot(ref (int x, int y) knot, (int x, int y) previousKnot)
    {
        if (Math.Abs(previousKnot.x - knot.x) > 1)
        {
            if (previousKnot.x < knot.x)
            {
                if (previousKnot.y > knot.y)
                {
                    knot.x -= 1;
                    knot.y += 1;
                } else if (previousKnot.y < knot.y)
                {
                    knot.x -= 1;
                    knot.y -= 1;
                }
                else
                {
                    knot.x -= 1;
                }
            }
            else
            {
                if (previousKnot.y > knot.y)
                {
                    knot.x += 1;
                    knot.y += 1;
                } else if (previousKnot.y < knot.y)
                {
                    knot.x += 1;
                    knot.y -= 1;
                }
                else
                {
                    knot.x += 1;    
                }
            }
        }

        if (Math.Abs(previousKnot.y - knot.y) > 1)
        {
            if (previousKnot.y < knot.y)
            {
                if (previousKnot.x > knot.x)
                {
                    knot.x += 1;
                    knot.y -= 1;
                } else if (previousKnot.x < knot.x)
                {
                    knot.x -= 1;
                    knot.y -= 1;
                }
                else
                {
                    knot.y -= 1;
                }
            }
            else
            {
                if (previousKnot.x > knot.x)
                {
                    knot.x += 1;
                    knot.y += 1;
                } else if (previousKnot.x < knot.x)
                {
                    knot.x -= 1;
                    knot.y += 1;
                }
                else
                {
                    knot.y += 1;
                }
            }
        }
    }
}