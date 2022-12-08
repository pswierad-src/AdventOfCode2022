var rawData = File.ReadAllText("input.txt");

var trees = rawData.Split(Environment.NewLine).ToList()
    .Select(x => x.ToCharArray()
        .Select(char.GetNumericValue)
        .ToList())
    .ToList();

var visibleOuterBound = 2 * trees.Count + 2 * trees.First().Count - 4;
var visibleInnerTrees = 0;

var bestTreeScore = 0;

var yAxis = trees.Count;
var xAxis = trees.First().Count();

for (int y = 1; y < yAxis - 1; y++)
{
    for (int x = 1; x < xAxis -1 ; x++)
    {
        var currentTree = trees[y][x];

        var top = trees.Select(t => t[x]).Take(y).ToList();
        var right = trees[y].TakeLast(xAxis - x - 1).ToList();
        var bottom = trees.Select(t => t[x]).TakeLast(yAxis-y-1).ToList();
        var left = trees[y].Take(x).ToList();

        if (left.All(t => t < currentTree)
            || right.All(t => t < currentTree)
            || top.All(t => t < currentTree)
            || bottom.All(t => t < currentTree))
        {
            visibleInnerTrees++;
        }

        top.Reverse();
        left.Reverse();

        var treeScore = CalculateScore(currentTree, top) 
                        * CalculateScore(currentTree, right) 
                        * CalculateScore(currentTree, bottom) 
                        * CalculateScore(currentTree, left);

        if (treeScore > bestTreeScore) bestTreeScore = treeScore;
    }
}

Console.WriteLine($"Trees visible from outside: {visibleOuterBound + visibleInnerTrees}");
Console.WriteLine($"Best tree score is: {bestTreeScore}");

Console.ReadLine();

int CalculateScore(double currentTree, List<double> sideTrees)
{
    var score = 0;
    
    foreach (var tree in sideTrees)
    {
        score++;
        
        if(tree < currentTree) continue;
        if (tree >= currentTree) break;
    }

    return score;
}