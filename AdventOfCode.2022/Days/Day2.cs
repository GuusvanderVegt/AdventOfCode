namespace AdventOfCode._2022.Days;

public class Day2 : BaseDay
{
    public Day2() : base("2")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var totalScore = 0;
        var totalScorePart2 = 0;
        foreach (var line in lines)
        {
            var opponentMove = line[0].ToString();
            var supposedMove = line[2].ToString();
           
            //Calculate score part 1
            switch (supposedMove)
            {
                case "X":
                {
                    
                    break;
                }
                case "Y":
                {
                    totalScore += (opponentMove == "A" ? 8 : opponentMove == "B" ? 5 : 2);
                    break;
                }
                case "Z":
                {
                    totalScore += (opponentMove == "A" ? 3 : opponentMove == "B" ? 9 : 6);
                    break;
                }
            }
            
            //Calculate score part 2
            switch (supposedMove)
            {
                case "X":
                {
                    totalScorePart2 += (opponentMove == "A" ? 3 : opponentMove == "B" ? 1 : 2);
                    break;
                }
                case "Y":
                {
                    totalScorePart2 += (opponentMove == "A" ? 4 : opponentMove == "B" ? 5 : 6);
                    break;
                }
                case "Z":
                {
                    totalScorePart2 += (opponentMove == "A" ? 8 : opponentMove == "B" ? 9 : 7);
                    break;
                }
            }
        }
        
        PrintSolution(1, totalScore.ToString());
        PrintSolution(2, totalScorePart2.ToString());
    }
}