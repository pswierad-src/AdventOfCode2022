var rawData = File.ReadAllLines("input.txt");

var regValues = new List<RegVal>();
var currentCycle = 0;
var xRegister = 1;

foreach (var op in rawData)
{
    var operation = op.Split(" ");

    switch (operation[0])
    {
        case "noop": NOOP(); break;
        case "addx": ADDX(int.Parse(operation[1])); break;
    }
}

var checkedValues = new List<RegVal>();

for (int i = 20; i < regValues.Count; i += 40)
{
    checkedValues.Add(regValues.First(x => x.Cycle == i));
}

Console.WriteLine(Environment.NewLine);
Console.WriteLine($"Sum value of checked signal strength is: {checkedValues.Select(x => x.Cycle * x.Value).Sum()}");

Console.ReadLine();

void NOOP()
{   
    WritePixel();
    currentCycle++;
    regValues.Add(new RegVal
    {
        Cycle = currentCycle,
        Value = xRegister
    });
}

void ADDX(int addedValue)
{
    WritePixel();
    currentCycle++;
    regValues.Add(new RegVal
    {
        Cycle = currentCycle,
        Value = xRegister
    });

    WritePixel();
    currentCycle++;
    regValues.Add(new RegVal
    {
        Cycle = currentCycle,
        Value = xRegister
    });

    xRegister += addedValue;
}

void WritePixel()
{
    var curX = (currentCycle % 40);
    var curY = currentCycle / 40;

    Console.SetCursorPosition(curX, curY);

    var sprite = new[] { xRegister - 1, xRegister, xRegister + 1 };

    Console.Write(sprite.Contains(curX) ? "#" : " ");
}

class RegVal
{
    public int Cycle { get; set; }
    public int Value { get; set; }
}