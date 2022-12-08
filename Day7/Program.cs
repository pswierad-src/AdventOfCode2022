var rawData = File.ReadAllLines("input.txt").ToList();
rawData = rawData.Skip(1).ToList();

List<Directory> dirs = new();

var currentDirectory = "/";

dirs.Add(new Directory
{
    Parent = currentDirectory,
    Name = currentDirectory,
    Files = new List<DirFile>(),
    SDirectories = new List<string>()
});

for (var i = 0; i < rawData.Count; i++)
{
    var command = rawData[i];
    
    if(command is "$ cd ..")
    {
        currentDirectory = MoveDirectoryUp(currentDirectory);
    } 
    else if (command.StartsWith("$ cd"))
    {
        var name = command.Split(' ')[2];
        AddDirectoryAndChangeToIt(name);
    } 
    else if(command.StartsWith("$ ls"))
    {
        AddFilesToDir(i);
    }
}

var totalSize = dirs.Select(dir => IsDirViable(dir, 100000, 0))
    .Where(viableUnder => viableUnder != null)
    .Sum(viableUnder => (int)viableUnder!);

Console.WriteLine($"Total size of directories smaller than 100000 is {totalSize}");

var homeDirSize = CountDirSize(dirs.First(x => x.Name == "/"), 0);
var availableSpace = 70000000 - homeDirSize;
var spaceToFree = 30000000 - availableSpace;


var potentialDirToRemoveSize = dirs.Select(dir => CountDirSize(dir, 0))
    .Where(size => size >= spaceToFree)
    .Prepend(homeDirSize)
    .Min();

Console.WriteLine($"Smallest directory size that deleting would free up enough space is {potentialDirToRemoveSize}");

Console.ReadLine();


int CountDirSize(Directory pDir, int previousSize)
{
    var currSize = previousSize + pDir.Files.Sum(file => int.Parse(file.Size));

    foreach (var cDir in pDir.SDirectories)
    {
        var isViableInContext = CountDirSize(dirs.First(x => x.Name == pDir.Name + "-" + cDir), currSize);

        currSize = isViableInContext;
    }

    return currSize;
}

int? IsDirViable(Directory pDir, int maxSize, int previousSize)
{
    var currSize = previousSize;
    
    foreach (var file in pDir.Files)
    {
        currSize += int.Parse(file.Size);
    }

    if (currSize > maxSize)
    {
        return null;
    }
    
    foreach (var cDir in pDir.SDirectories)
    {
        var isViableInContext = IsDirViable(dirs.First(x => x.Name == pDir.Name + "-" + cDir), maxSize, currSize);

        if (isViableInContext is null)
        {
            return null;
        }

        currSize = (int)isViableInContext;
    }

    return currSize;
}

string MoveDirectoryUp(string dir)
{
    var currentDir = dirs.First(x => x.Name == dir);

    return currentDir.Parent;
}

void AddDirectoryAndChangeToIt(string name)
{
    if (string.IsNullOrEmpty(name))
    {
        throw new Exception("Something is wrong");
    }

    if (dirs.Any(x => x.Name != name))
    {
        dirs.Add(new Directory
        {
            Parent = currentDirectory,
            Name = currentDirectory+ "-" + name,
            Files = new List<DirFile>(),
            SDirectories = new List<string>()
        });
    }

    currentDirectory = currentDirectory+ "-" + name;
}

void AddFilesToDir(int index)
{
    var currentDir = dirs.First(x => x.Name == currentDirectory);

    var endOfFiles = 0;
    
    for (var i = index +1; i < rawData.Count-1; i++)
    {
        endOfFiles = i;
        if (rawData[i].StartsWith("$"))
        {
            break;
        }
    }

    if (endOfFiles == 0)
    {
        return;
    }

    var filesInDir = rawData.GetRange(index + 1, endOfFiles - index-1);

    foreach (var file in filesInDir)
    {
        var fileData = file.Split(' ');

        if (int.TryParse(fileData[0], out _))
        {
            currentDir.Files.Add(new DirFile
            {
                Size = fileData[0],
                Name = fileData[1]
            });
        }
        else if(fileData[0] is "dir")
        {
            currentDir.SDirectories.Add(fileData[1]);
        }
    }
}


class Directory
{
    public string Parent { get; set; }
    public string Name { get; set; }
    public List<DirFile> Files { get; set; } //Name Type
    public List<string> SDirectories { get; set; }
}

class DirFile
{
    public string Size { get; set; }
    public string Name { get; set; }
}