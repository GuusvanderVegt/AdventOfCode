namespace AdventOfCode._2024.Days;

public class Day6 : BaseDay
{
    public Day6() : base("06")
    {
    }
    
    protected override void Solve(List<string> lines)
    {
        var map = lines.Select(x => x.ToCharArray()).ToArray();
        var guardPosition = GetInitialGuardPosition(map);
        var direction = MoveDirection.Up; //TODO: Maybe this can differ in the input?
        
        while (true)
        {
            var nextPosition = GetNextPosition(guardPosition, direction);
            //Check if next position is out of bounds (guard has moved off the map)
            if (nextPosition.Y < 0 || nextPosition.Y >= map.Length || nextPosition.X < 0 || nextPosition.X >= map[nextPosition.Y].Length)
            {
                break;
            }
            
            if (map[nextPosition.Y][nextPosition.X] == '#')
            {
                direction = direction.MakeTurn();
            }
            else
            {
                map[guardPosition.Y][guardPosition.X] = '1';
                guardPosition = nextPosition;
            }
        }
        
        //Count the number of visited cells
        var visitedCells = map.SelectMany((x) => x).Count(x => x == '1') + 1; //+1 for the starting position
        PrintSolution(1, visitedCells.ToString());
        
        // Part 2
        var visitedPoints = map.SelectMany((x, Y) => x.Where(c => c == '1').Select((_, X) => (X, Y)));
       
        var possibleObstacles = new List<(int Y, int X)>();
        foreach (var point in visitedPoints)
        {
            // Reset the playing field
            map = lines.Select(x => x.ToCharArray()).ToArray();
            var guardPosition2 = GetInitialGuardPosition(map);
            var direction2 = MoveDirection.Up;
            
            // Try to set the point as a new obstacle
            map[point.Y][point.X] = '0';
            
            var visitedDirectedPoints = new List<(int Y, int X, MoveDirection Direction)>();
            
            while (true)
            {
                //Is in loop (guard has visited this point before with the same direction)
                if (visitedDirectedPoints.Any(x => x.Y == guardPosition2.X && x.X == guardPosition2.Y && x.Direction == direction2))
                {
                    possibleObstacles.Add(point);
                    break;
                }
                
                var nextPosition = GetNextPosition(guardPosition2, direction2);
                //Check if next position is out of bounds (guard has moved off the map)
                if (nextPosition.Y < 0 || nextPosition.Y >= map.Length || nextPosition.X < 0 || nextPosition.X >= map[nextPosition.Y].Length)
                {
                    break;
                }
            
                visitedDirectedPoints.Add((guardPosition2.X, guardPosition2.Y, direction2));
                if (map[nextPosition.Y][nextPosition.X] == '#' || map[nextPosition.Y][nextPosition.X] == '0')
                {
                    direction2 = direction2.MakeTurn();
                }
                else
                {
                    guardPosition2 = nextPosition;
                }
            }
        }
        
        PrintSolution(2, possibleObstacles.Count.ToString());
        
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

static class MoveDirectionExtensions
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
}

