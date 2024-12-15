namespace AdventOfCode._2024.Days;

public class Day6 : BaseDay
{
    private char[][] _originalMap;
    private (int X, int Y) _guardPosition;
    public Day6() : base("06")
    {
    }
    
    protected override void Solve(List<string> lines)
    {
        // Part 1
        _originalMap = lines.Select(x => x.ToCharArray()).ToArray();
        _guardPosition = GetInitialGuardPosition(_originalMap);
        
        var visitedCells = Move(_originalMap); 
        PrintSolution(1, visitedCells.Points.Count.ToString());
        
        // Part 2
        var possibleObstacles = 0;
        foreach (var point in visitedCells.Points)
        {
            // Skip the point if it is the starting position
            if(_guardPosition.X == point.X && _guardPosition.Y == point.Y)
            {
                continue;
            }

            var map = _originalMap.CopyMap();
            
            // Try to set the point as a new obstacle
            map[point.Y][point.X] = '0';
            
            var (_, isLoop) = Move(map);
            if (isLoop)
            {
                possibleObstacles++;
            }
        }
        
        PrintSolution(2, possibleObstacles.ToString());
    }

    private (HashSet<(int X, int Y)> Points, bool IsLoop) Move(char[][] map)
    {
        var guardPosition = _guardPosition;
        var direction = MoveDirection.Up;
        var isLoop = false;
        
        var visitedPoints = new HashSet<(int X, int Y)>();
        var visitedStates = new HashSet<(int X, int Y, MoveDirection Direction)>();
        
        while (true)
        {
            var nextPosition = GetNextPosition(guardPosition, direction);
            
            //Check if next position is out of bounds (guard has moved off the map)
            if (nextPosition.Y < 0 || nextPosition.Y >= map.Length || nextPosition.X < 0 || nextPosition.X >= map[nextPosition.Y].Length)
            {
                break;
            } 
            
            //Guard has reached an obstacle
            if (map[nextPosition.Y][nextPosition.X] == '#' || map[nextPosition.Y][nextPosition.X] == '0')
            {
                direction = direction.MakeTurn();
            }
            //Guard is free to move
            else
            {
                guardPosition = nextPosition;

                // If the guard has visited this point before with the same direction, it is in a loop (a hashset can only contain unique values, so it returns false if the point has been added before)
                if (!visitedStates.Add((guardPosition.X, guardPosition.Y, direction)))
                {
                    isLoop = true;
                    break;
                }
                
                visitedPoints.Add((guardPosition.X, guardPosition.Y));
            }
        }

        return (visitedPoints, isLoop);
    }
    
    private static (int X, int Y) GetInitialGuardPosition(char[][] map)
    {
        for (var y = 0; y < map.Length; y++)
        {
            for (var x = 0; x < map[y].Length; x++)
            {
                if (map[y][x] == '^')
                {
                    return (x, y);
                }
            }
        }

        throw new Exception("Guard not found");
    }

    private static (int X, int Y) GetNextPosition((int X, int Y) currentPosition, MoveDirection direction) =>
        direction switch
        {
            MoveDirection.Up => (currentPosition.X, currentPosition.Y - 1),
            MoveDirection.Right => (currentPosition.X + 1, currentPosition.Y),
            MoveDirection.Down => (currentPosition.X, currentPosition.Y + 1),
            MoveDirection.Left => (currentPosition.X - 1, currentPosition.Y),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };


}

enum MoveDirection
{
    Up = 0,
    Right = 1,
    Down = 2,
    Left = 3
}

internal static class Day6Extensions
{
    public static MoveDirection MakeTurn(this MoveDirection direction)
    {
        return direction switch
        {
            MoveDirection.Up => MoveDirection.Right,
            MoveDirection.Right => MoveDirection.Down,
            MoveDirection.Down => MoveDirection.Left,
            MoveDirection.Left => MoveDirection.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public static char[][] CopyMap(this char[][] map)
    {
        var newMap = new char[map.Length][];
        for (int i = 0; i < map.Length; i++)
        {
            newMap[i] = new char[map[i].Length];
            Array.Copy(map[i], newMap[i], map[i].Length);
        }
        return newMap;
    }
}

