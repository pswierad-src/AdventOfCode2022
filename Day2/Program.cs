var rawGameData = File.ReadAllText("input.txt").Split(
        Environment.NewLine,
        StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(" ")).ToList();

var points = 0;

foreach (var game in rawGameData)
{
    switch (game[1])
    {
        // I played rock
        case "X":
            points += 1;
            points = game[0] switch
            {
                "A" => points + 3,
                "B" => points + 0,
                "C" => points + 6
            };
            break;
        
        // I played paper
        case "Y":
            points += 2;
            points = game[0] switch
            {
                "A" => points + 6,
                "B" => points + 3,
                "C" => points + 0
            };
            break;
        
        // I played scissors
        case "Z":
            points += 3;
            points = game[0] switch
            {
                "A" => points + 0,
                "B" => points + 6,
                "C" => points + 3
            };
            break;
    }
}

Console.WriteLine($"Total points first method: {points}");

points = 0;

foreach (var game in rawGameData)
{
    switch(game[1])
    {
        // I loose
        case "X":
            points += 0;
            points = game[0] switch
            {
                "A" => points + 3,
                "B" => points + 1,
                "C" => points + 2
            };
            break;
        
        // I draw
        case "Y":
            points += 3;
            points = game[0] switch
            {
                "A" => points + 1,
                "B" => points + 2,
                "C" => points + 3
            };
            break;
        
        // I win
        case "Z":
            points += 6;
            points = game[0] switch
            {
                "A" => points + 2,
                "B" => points + 3,
                "C" => points + 1
            };
            break;
    }
}

Console.WriteLine($"Total points new method: {points}");

Console.ReadLine();
