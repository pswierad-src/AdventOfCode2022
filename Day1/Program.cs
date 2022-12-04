var rawData = File.ReadAllText("calories.txt").Split(
    new string[] { Environment.NewLine + Environment.NewLine },
    StringSplitOptions.RemoveEmptyEntries).ToList();

var valueOfCaloriesKeptByElf = rawData
    .Select(x => x.Split(Environment.NewLine).Where(y => !string.IsNullOrEmpty(y)));

var elfCalInvSum = valueOfCaloriesKeptByElf
    .Select(x => x.Select(y => Convert.ToInt32(y)).Sum()).ToList();
    
// Largest amount of calories
Console.WriteLine($"Largest amount of calories: {elfCalInvSum.Max()}");

// Top 3 calories holders
var top3CalHolders = elfCalInvSum.OrderDescending().Take(3);

Console.WriteLine($"Top 3 elves have: {top3CalHolders.Sum()} calories.");

Console.ReadLine();