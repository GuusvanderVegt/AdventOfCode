using System.Drawing;

namespace AdventOfCode._2022.Days;

public class Day9 : BaseDay
{
    public Day9() : base("9")
    {
    }

    protected override void Solve(List<string> lines)
    {
        Part1(lines);
        
        var knots = new Point[10];
        
        var tailPositions = new List<Point>()
        {
            new(0,0)
        };

        foreach (string line in lines)
        {
            var parts = line.Split(" ");
            var direction = parts[0];
            var amountOfSteps = int.Parse(parts[1]);
            
            for (int i = 0; i < amountOfSteps; i++)
            {
                
                var oldHead = knots.First();

                if (direction == "R")
                {
                    knots[0].X++;
                }
                else if (direction == "L")
                {
                    knots[0].X--;
                }
                else if (direction == "U")
                {
                    knots[0].Y++;
                }
                else if (direction == "D")
                {
                    knots[0].Y--;
                }

                for (int j = 1; j < knots.Length; j++)
                {
                    if (TailIsInReachOfHead(knots[j - 1], knots[j])) continue;
                    
                    var knot = new Point(knots[j].X, knots[j].Y);
                    knots[j] = oldHead;
                    oldHead = knot;

                    if (j == knots.Length - 1)
                    {
                        if (!PointsContainCoordinates(tailPositions, knots.Last()))
                        {
                            var newPoint = new Point(knots.Last().X, knots.Last().Y);
                            tailPositions.Add(newPoint);
                        }
                    }
                        
                }
            }
        }
        
        PrintSolution(2, tailPositions.Count.ToString());
    }

    void Part1(List<string> lines)
    {
        var head = new Point(0, 0);
        var tail = new Point(0, 0);
        var tailPositions = new List<Point>()
        {
            new Point(0, 0)
        };
        

        foreach (var line in lines)
        {
            var parts = line.Split(" ");
            var direction = parts[0];
            var amountOfSteps = int.Parse(parts[1]);
            
            for (int i = 0; i < amountOfSteps; i++)
            {
                var oldHead = head;

                if (direction == "R")
                {
                    head.X++;
                }
                else if (direction == "L")
                {
                    head.X--;
                }
                else if (direction == "U")
                {
                    head.Y++;
                }
                else if (direction == "D")
                {
                    head.Y--;
                }

                if (TailIsInReachOfHead(head, tail)) continue;

                tail = oldHead;

                if (!PointsContainCoordinates(tailPositions, tail))
                {
                    var newPoint = new Point(tail.X, tail.Y);
                    tailPositions.Add(newPoint);
                }

            }
            
        }
        
        PrintSolution(1, tailPositions.Count.ToString());
    }
    
    
    bool TailIsInReachOfHead(Point head, Point tail)
    {
        //Check if tail is within reach of head
        var xDifference = Math.Abs(head.X - tail.X);
        var yDifference = Math.Abs(head.Y - tail.Y);
        
        return xDifference <= 1 && yDifference <= 1;
    }
    
    bool PointsContainCoordinates(List<Point> points, Point point)
    {
        return points.Any(p => p.X == point.X && p.Y == point.Y);
    }
    
}

