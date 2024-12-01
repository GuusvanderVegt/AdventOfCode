namespace AdventOfCode._2022.Days;

public class Day8 : BaseDay
{
    public Day8() : base("8")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var treeGrid = lines.Select(x => x.Select(c => int.Parse(c.ToString())).ToList()).ToList();
        
        var solutionPart1 = treeGrid.Count + treeGrid.Count + (treeGrid[0].Count - 2) + (treeGrid[0].Count - 2);
        var listPart2 = new List<int>();

        for (var row = 1; row < treeGrid.Count - 1; row++)
        {
            for (var col = 1; col < treeGrid[row].Count - 1; col++)
            {
                //Part 1
                if (SolutionPart1(treeGrid, row, col)) solutionPart1++;
                
                //Part 2
                listPart2.Add(GetViewDistance(treeGrid, row, col));
            }
        }
        
        PrintSolution(1, solutionPart1.ToString());
        PrintSolution(2, listPart2.Max().ToString());
    }

    private static int GetViewDistance(List<List<int>> treeGrid, int row, int col)
    {
        var treeHeight = treeGrid[row][col];

        var viewingDistanceLeft = 0;
        var viewingDistanceRight = 0;
        var viewingDistanceUp = 0;
        var viewingDistanceDown = 0;

        // Left
        for (var i = col - 1; i >= 0; i--)
        {
            viewingDistanceLeft++;
            if (treeGrid[row][i] >= treeHeight)
            {
                break;
            }
        }

        // Right
        for (var i = col + 1; i < treeGrid[row].Count; i++)
        {
            viewingDistanceRight++;
            if (treeGrid[row][i] >= treeHeight)
            {
                break;
            }
        }

        // Up
        for (var i = row - 1; i >= 0; i--)
        {
            viewingDistanceUp++;
            if (treeGrid[i][col] >= treeHeight)
            {
                break;
            }
        }

        // Down
        for (var i = row + 1; i < treeGrid.Count; i++)
        {
            viewingDistanceDown++;
            if (treeGrid[i][col] >= treeHeight)
            {
                break;
            }
        }

        return viewingDistanceLeft * viewingDistanceRight * viewingDistanceUp * viewingDistanceDown;
    }

    private static bool SolutionPart1(List<List<int>> treeGrid, int row, int col)
    {
        var treeHeight = treeGrid[row][col];

        var leftValid = true;
        var rightValid = true;
        var topValid = true;
        var bottomValid = true;

        //Check left
        for (var i = 0; i < col; i++)
        {
            var checkHeight = treeGrid[row][i];

            if (checkHeight < treeHeight) continue;

            leftValid = false;
            break;
        }

        //Check right
        for (var i = col + 1; i < treeGrid[row].Count; i++)
        {
            var checkHeight = treeGrid[row][i];

            if (checkHeight < treeHeight) continue;

            rightValid = false;
            break;
        }

        //Check top
        for (var i = 0; i < row; i++)
        {
            var checkHeight = treeGrid[i][col];

            if (checkHeight < treeHeight) continue;

            topValid = false;
            break;
        }

        //Check bottom
        for (var i = row + 1; i < treeGrid.Count; i++)
        {
            var checkHeight = treeGrid[i][col];

            if (checkHeight < treeHeight) continue;

            bottomValid = false;
            break;
        }

        return (leftValid || rightValid || topValid || bottomValid);
        
    }
}