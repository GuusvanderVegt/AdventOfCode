namespace AdventOfCode._2022.Days;

public class Day7 : BaseDay
{
    public Day7() : base("7")
    {
    }

    protected override void Solve(List<string> lines)
    {
        var currentDir = new Directory();
        
        foreach (var line in lines)
        {
            if (line.Contains("$ ls")) continue;

            if (line.Contains("$"))
            {
                if(line.Contains("$ cd /"))
                {
                    currentDir = Directory.RootDir;
                    continue;
                }

                if (line.Contains("cd .."))
                {
                    currentDir = currentDir.Name == "/" ? currentDir : currentDir.Parent;
                    continue;
                }

                var newDir = new Directory()
                {
                    Name = line.Split(" ")[2],
                    Files = new List<AocFile>(),
                    SubDirectories = new List<Directory>(),
                    Parent = currentDir
                };
                
                currentDir.SubDirectories.Add(newDir);
                currentDir = newDir;
                continue;
            }

            if (line.Contains("dir"))
            {
                var newDir = new Directory()
                {
                    Name = line.Split(" ")[1],
                    Files = new List<AocFile>(),
                    SubDirectories = new List<Directory>(),
                    Parent = currentDir
                };
                
                currentDir.SubDirectories.Add(newDir);
                continue;
            }
            
            var fileSize = line.Split(" ")[0];
            var fileName = line.Split(" ")[1];
            
            var file = new AocFile()
            {
                Name = fileName,
                Size = int.Parse(fileSize)
            };
            
            currentDir.Files.Add(file);
        }

        var totalSpaceLeft = 70000000 - Directory.RootDir.Size();
        var neededSpace = 30000000 - totalSpaceLeft;
        var dirSizes = GetAllDirsSize(Directory.RootDir);
        dirSizes.Sort();
        

        PrintSolution(1, GetTotalDirsSizeDirectorySizeSmallerOREqualHundredThousand(Directory.RootDir).ToString());
        PrintSolution(2, dirSizes.FirstOrDefault(size => size >= neededSpace).ToString());
    }
    
    private static int GetTotalDirsSizeDirectorySizeSmallerOREqualHundredThousand(Directory dir) => (dir.Size() <= 100000 ? dir.Size() : 0) + dir.SubDirectories.Sum(GetTotalDirsSizeDirectorySizeSmallerOREqualHundredThousand);
    private static List<int> GetAllDirsSize(Directory dir) => dir.SubDirectories.SelectMany(GetAllDirsSize).Concat(new List<int>() {dir.Size()}).ToList();
    
}

class Directory
{
    public string Name { get; set; } = default!;
    public List<Directory> SubDirectories { get; set; } = default!;
    public List<AocFile> Files { get; set; } = default!;
    public Directory? Parent { get; set; }

    public int Size() => Files.Sum(x => x.Size) + SubDirectories.Sum(x => x.Size());

    public static Directory RootDir =  new()
    {
        Name = "/",
        SubDirectories = new List<Directory>(),
        Files = new List<AocFile>(),
        Parent = null,
    };
    
}

class AocFile
{
    public string Name { get; set; } = default!;
    public int Size { get; set; } = default!;
}