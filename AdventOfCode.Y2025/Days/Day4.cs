namespace AdventOfCode.Y2025.Days;

public class Day4 : BaseDay
{
    public Day4()
        : base("04") { }

    protected override void Solve(List<string> lines)
    {
        char[][] grid = lines.Select(line => line.ToCharArray()).ToArray();
        Part1(grid);
        Part2(grid);
    }

    private static void Part1(char[][] grid)
    {
        var reachableRolls = GetReachableRolls(grid);
        PrintSolution(1, reachableRolls.Count.ToString());
    }
    
    private static void Part2(char[][] grid)
    {
        var answerPart2 = 0;
        bool hasEdited;

        do
        {
            hasEdited = false;
            
            var reachableRolls = GetReachableRolls(grid);
            if (reachableRolls.Count == 0)
            {
                continue;
            }
            
            hasEdited = true;
            answerPart2+= reachableRolls.Count;
            
            foreach (var (x, y) in reachableRolls)
            {
                grid[x][y] = 'X'; // Mark as removed
            }
            
        } while (hasEdited);
        
        
        PrintSolution(2, answerPart2.ToString());
    }

    private static List<(int, int)> GetReachableRolls(char[][] grid)
    {
        List<(int, int)> reachable = [];
        for (int i = 0; i < grid.Length; i++)
        {
            var row = grid[i];
            for (int j = 0; j < row.Length; j++)
            {
                char current = row[j];
                if (current != '@') continue;
                var adjacentRolls = GetAdjacentRollCount(grid, i, j);
                if (adjacentRolls < 4)
                {
                    reachable.Add((i, j));
                }
            }
        }
        
        return reachable;
    }

    private static int GetAdjacentRollCount(char[][] grid, int x, int y)
    {
        // Check if adjacent cells contain a roll (represented by '@')
        int count = 0;
        int rows = grid.Length;
        int cols = grid[0].Length;
        List<(int X, int Y)> directions =
        [
            (-1, 0), (1, 0), (0, -1), (0, 1), (-1, -1), (-1, 1), (1, -1), (1, 1)
        ];

        foreach (var dir in directions)
        {
            int newX = x + dir.X;
            int newY = y + dir.Y;
            if (newX < 0 || newX >= rows || newY < 0 || newY >= cols) continue;
            if (grid[newX][newY] == '@')
            {
                count++;
            }
        }
        
        return count;
    }
}
