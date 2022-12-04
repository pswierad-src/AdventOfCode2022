var rawData = File.ReadAllLines("input.txt");

List<char> duplicateItems = new();

foreach (var rucksackItems in rawData)
{
    var compartments = rucksackItems.Insert(rucksackItems.Length / 2, " ").Split(" ");

    var firstItems = compartments[0].ToCharArray();

    var duplicatesToAdd = new List<char>();
    
    foreach (var item in firstItems)
    {
        if (compartments[1].Contains(item) && !duplicatesToAdd.Contains(item))
        {
            duplicatesToAdd.Add(item);
        }
    }
    
    duplicateItems.AddRange(duplicatesToAdd);
}


Console.WriteLine($"Total priority of duplicate items is: {SumPriority(duplicateItems)}");

if (rawData.Length % 3 != 0)
{
    throw new Exception("Elven backpacks are not divisible into 3's");
}

List<char> badges = new();
for (int i = 0; i < rawData.Length-2; i+=3)
{
    var firstElf = rawData[i].ToCharArray();
    var secondElf = rawData[i + 1];
    var thirdElf = rawData[i + 2];

    foreach (var item in firstElf)
    {
        if (secondElf.Contains(item) && thirdElf.Contains(item))
        {
            badges.Add(item);
            break;
        }
    }
}

Console.WriteLine($"Total priority of badges is: {SumPriority(badges)}");
Console.ReadLine();

int SumPriority(List<char> items)
{
    var priority = 0;
    
    foreach (var item in items)
    {
        if (char.IsUpper(item))
        {
            priority += (item - 38);
        }
        else
        {
            priority += item - 96;
        }
    }

    return priority;
}