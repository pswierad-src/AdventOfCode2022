var rawData = File.ReadAllLines("input.txt");

var elfPairs = rawData.Select(x => x.Split(","));

var overlapCompletelyPairs = 0;
var overlapPartiallyPairs = 0;

foreach (var elfPair in elfPairs)
{
    var firstElfSector = elfPair[0].Split("-").Select(x => Convert.ToInt32(x)).ToList();
    var secondElfSector = elfPair[1].Split("-").Select(x => Convert.ToInt32(x)).ToList();

    if ((firstElfSector[0] >= secondElfSector[0] && firstElfSector[1] <= secondElfSector[1])
        || (firstElfSector[0] <= secondElfSector[0] && firstElfSector[1] >= secondElfSector[1]))
    {
        overlapCompletelyPairs++;
    }

    if (!(firstElfSector[1] < secondElfSector[0] || firstElfSector[0] > secondElfSector[1]))
    {
        overlapPartiallyPairs++;
    }
}

Console.WriteLine($"Overlapping completely pairs: {overlapCompletelyPairs}");
Console.WriteLine($"Overlapping partially pairs: {overlapPartiallyPairs}");

Console.ReadLine();