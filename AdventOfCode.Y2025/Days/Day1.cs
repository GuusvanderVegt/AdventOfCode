namespace AdventOfCode.Y2025.Days;

public class Day1 : BaseDay
{
    public Day1()
        : base("01") { }

    protected override void Solve(List<string> lines)
    {
        var answerPart1 = 0;
        var answerPart2 = 0;
        
        int position = 50;
        foreach (var line in lines)
        {
            var direction = line[0];
            var distance = int.Parse(line[1..]);
            for (int i = 0; i < distance; i++)
            {
                if (direction == 'L')
                    position -= 1;
                else if (direction == 'R')
                    position += 1;

                if (position < 0)
                {
                    position = (position + 100) % 100;
                }
                else if (position > 99)
                {
                    position %= 100;
                }
                
                if (position == 0)
                {
                    answerPart2++;
                }
            }

            if (position == 0)
            {
                answerPart1++;
            }
        }

        PrintSolution(1, answerPart1.ToString());
        PrintSolution(2, answerPart2.ToString());
    }
}
