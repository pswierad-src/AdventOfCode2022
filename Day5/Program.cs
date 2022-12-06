var rawData = File.ReadAllText("input.txt");

var rawCrates = rawData.Split(Environment.NewLine + Environment.NewLine)[0];
var rawInstructions = rawData.Split(Environment.NewLine + Environment.NewLine)[1];

var crates = ParseRawDataToStacks(rawCrates);
var instructions = rawInstructions.Split(Environment.NewLine);

foreach (var instruction in instructions)
{
    var digits = new string(instruction.ToCharArray().Where(x => char.IsDigit(x) || x == ' ').ToArray()).Split("  ");

    var move = int.Parse(digits[0]);
    var from = int.Parse(digits[1]);
    var to = int.Parse(digits[2]);
    
    // CrateMover 9000
    // for (int i = 0; i < move; i++)
    // {
    //     var item = crates[from - 1].First();
    //     crates[from-1].RemoveAt(0);
    //     
    //     crates[to-1] = crates[to-1].Prepend(item).ToList();
    // }
    
    // CrateMover 9001
    var items = crates[from - 1].Take(move).ToList();

    crates[from - 1] = crates[from - 1].Skip(move).ToList();
    
    items.AddRange(crates[to-1]);
    crates[to - 1] = items;
}

var answer = new string(crates.Select(x => x.First()).ToArray());
Console.WriteLine($"Top crates are {answer}");

Console.ReadLine();

List<List<char>> ParseRawDataToStacks(string data)
{
    var stacks = new List<List<char>>();
    
    var rows = data.Split(Environment.NewLine);

    var columns = rows.Take(rows.Length - 1).ToList();
    var indexes = rows.Last().ToCharArray();

    foreach (var c in indexes)
    {
        if (!int.TryParse(c.ToString(), out var num))
        {
            continue;
        }

        var indexOf = rows.Last().IndexOf(c);
        
        stacks.Add(columns.Select(x => x[indexOf]).Where(x => x != ' ').ToList());
    }

    return stacks;
}

