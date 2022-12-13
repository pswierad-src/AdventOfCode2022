var rawData = File.ReadAllLines("input.txt")
    .Select(x => x.ToCharArray())
    .ToArray();

var startPos = new Pos(-1, -1);
var trailStartPositions = new List<Pos>();
var endPos = new Pos(-1, -1);

var heights = new int[rawData.Length, rawData.First().Length];

for (int y = 0; y < rawData.Length; y++)
{
    for (int x = 0; x < rawData.First().Length; x++)
    {
        if (rawData[y][x] is 'S') startPos = new Pos(y, x);
        if (rawData[y][x] is 'E') endPos = new Pos(y, x);
        
        if(rawData[y][x] is 'a') trailStartPositions.Add(new Pos(y,x));

        heights[y, x] = rawData[y][x] switch
        {
            'E' => 'z' - 'a',
            'S' => 0,
            _ => rawData[y][x] - 'a'
        };
    }
}

trailStartPositions.Add(startPos);

Console.WriteLine("Part1");
var pathLength = FindPath(startPos);
Console.WriteLine($"Path length is {pathLength} steps long." + Environment.NewLine);

Console.WriteLine("Part2");
var foundPaths = trailStartPositions.Select(FindPath).ToList();
Console.WriteLine($"Shortest path for hiking trail is {foundPaths.Where(x => x > 0).Min()} steps long.");

Console.ReadLine();


int FindPath(Pos startingPosition)
{
    var paths = new int[rawData.Length, rawData.First().Length];

    for (int y = 0; y < rawData.Length; y++)
    {
        for (int x = 0; x < rawData.First().Length; x++)
        {
            paths[y, x] = -1;
        }
    }

    paths[startingPosition.Y, startingPosition.X] = 0;

    var queue = new Queue<Pos>();
    queue.Enqueue(startingPosition);

    while (queue.Count > 0)
    {
        var currentPos = queue.Dequeue();

        if (currentPos.X == endPos.X && currentPos.Y == endPos.Y)
        {
            return paths[currentPos.Y, currentPos.X];
        }

        foreach (var checkedPos in currentPos.PotentialSteps)
        {
            if(!MapContains(checkedPos, rawData.Length, rawData.First().Length)) continue;
            if(!ValidMove(currentPos, checkedPos)) continue;

            if (paths[checkedPos.Y, checkedPos.X] is -1)
            {
                paths[checkedPos.Y, checkedPos.X] = paths[currentPos.Y, currentPos.X] + 1;
                queue.Enqueue(checkedPos);
            }
        }
    }

    return -1;
}

bool MapContains(Pos pos, int maxY, int maxX)
    => pos.Y >= 0 & pos.Y < maxY && pos.X >= 0 && pos.X < maxX;

bool ValidMove(Pos currentPos, Pos nextPos)
{
    return heights[nextPos.Y, nextPos.X] <= heights[currentPos.Y, currentPos.X] + 1;
}

record Pos(int Y, int X)
{
    public Pos Top => this with { Y = Y - 1 };
    public Pos Bottom => this with { Y = Y + 1 };
    public Pos Right => this with { X = X + 1 };
    public Pos Left => this with { X = X - 1 };

    public List<Pos> PotentialSteps => new() { Top, Bottom, Right, Left };
}












