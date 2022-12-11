var rawData = File.ReadAllLines("input.txt");

var tailVisitedCoords = new List<Coords>();

var rope = new List<Coords>()
{
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
    new Coords { X = 20, Y = 20 },
};

tailVisitedCoords.Add(new Coords { X = rope.Last().X, Y = rope.Last().Y});

foreach (var data in rawData)
{
    var move = data.Split(" ");

    var direction = move[0];
    var steps = int.Parse(move[1]);

    for (int i = 0; i < steps; i++)
    {
        MoveHeadDirection(direction);

        for (int k = 1; k < rope.Count; k++)
        {
            AdjustKnot(k);
        }
    }

}

Console.WriteLine($"Tail has visited: {tailVisitedCoords.Count} unique positions.");
Console.ReadLine();

void AdjustKnot(int currentKnot)
{
    var distance = new Coords
    {
        X = rope[currentKnot-1].X - rope[currentKnot].X,
        Y = rope[currentKnot-1].Y - rope[currentKnot].Y
    };

    if (distance.X < 0 || distance.Y < 0)
    {
        
    }

    if (Math.Abs(distance.Y) < 2 && Math.Abs(distance.X) < 2)
    {
        return;
    }

    if (distance.X != 0)
    {
        if (Math.Abs(distance.X) == 1)
        {
            rope[currentKnot].X += distance.X;
        }
        else
        {
            rope[currentKnot].X += distance.X > 0 ? distance.X - 1 : distance.X + 1;
        }
    }

    if (distance.Y != 0)
    {
        if (Math.Abs(distance.Y) == 1)
        {
            rope[currentKnot].Y += distance.Y;
        }
        else
        {
            rope[currentKnot].Y += distance.Y > 0 ? distance.Y - 1 : distance.Y + 1;
        }
    }

    if (currentKnot != rope.Count - 1) return;

    if (!tailVisitedCoords.Any(c => c.X == rope[currentKnot].X && c.Y == rope[currentKnot].Y))
    {
        tailVisitedCoords.Add(new Coords {X = rope[currentKnot].X, Y = rope[currentKnot].Y});
    }
}

void MoveHeadDirection(string direction)
{
    switch (direction)
    {
        case "U":
            rope.First().Y++;
            break;
        case "R":
            rope.First().X++;
            break;
        case "D" :
            rope.First().Y--;
            break;
        case "L":
            rope.First().X--;
            break;
    }
}

class Coords
{
    public int X { get; set; }
    public int Y { get; set; }
}