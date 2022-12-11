var monkeys = new List<Monkey>
{
    new Monkey
    {
        Number = 0,
        Items = new List<long> { 74, 73, 57, 77, 74},
        Operation = i => i*11,
        DivisibleBy = 19,
        TrueThrowTo = 6,
        FalseThrowTo = 7
    },
    new Monkey
    {
        Number = 1,
        Items = new List<long> { 99, 77, 79},
        Operation = i => i+8,
        DivisibleBy = 2,
        TrueThrowTo = 6,
        FalseThrowTo = 0
    },
    new Monkey
    {
        Number = 2,
        Items = new List<long> { 64, 67, 50, 96, 89, 82, 82},
        Operation = i => i+1,
        DivisibleBy = 3,
        TrueThrowTo = 5,
        FalseThrowTo = 3
    },
    new Monkey
    {
        Number = 3,
        Items = new List<long> { 88 },
        Operation = i => i*7,
        DivisibleBy = 17,
        TrueThrowTo = 5,
        FalseThrowTo = 4
    },
    new Monkey
    {
        Number = 4,
        Items = new List<long> { 80, 66, 98, 83, 70, 63, 57, 66 },
        Operation = i => i+4,
        DivisibleBy = 13,
        TrueThrowTo = 0,
        FalseThrowTo = 1
    },
    new Monkey
    {
        Number = 5,
        Items = new List<long> { 81, 93, 90, 61, 62, 64 },
        Operation = i => i+7,
        DivisibleBy = 7,
        TrueThrowTo = 1,
        FalseThrowTo = 4
    },
    new Monkey
    {
        Number = 6,
        Items = new List<long> { 69, 97, 88, 93 },
        Operation = i => i*i,
        DivisibleBy = 5,
        TrueThrowTo = 7,
        FalseThrowTo = 2
    },
    new Monkey
    {
        Number = 7,
        Items = new List<long> { 59, 80 },
        Operation = i => i+6,
        DivisibleBy = 11,
        TrueThrowTo = 2,
        FalseThrowTo = 3
    }
};

Monkey.LCM = LCM(monkeys.Select(m => m.DivisibleBy).ToArray());

for (int i = 1; i <= 10000; i++)
{
    Round();
}


foreach (var monkey in monkeys)
{
    Console.WriteLine($"Monkey {monkey.Number} inspected {monkey.InspectCount} items.");
}

var top2 = monkeys.OrderByDescending(x => x.InspectCount).Take(2).ToList();
Console.WriteLine($"Monkey business level is {top2[0].InspectCount * top2[1].InspectCount}");

Console.ReadLine();

void Round()
{
    foreach (var monkey in monkeys)
    {
        foreach (var item in monkey.Items)
        {
            //var worryLevel = monkey.Operation(item) / 3;
            var worryLevel = monkey.Operation(item) % Monkey.LCM;
            
            if (worryLevel % monkey.DivisibleBy == 0)
            {
                monkeys.First(x => x.Number == monkey.TrueThrowTo).Items.Add(worryLevel);
            }
            else
            {
                monkeys.First(x => x.Number == monkey.FalseThrowTo).Items.Add(worryLevel);
            }
            
            monkey.InspectCount++;
        }

        monkey.Items = new List<long>();
    }
}


static long LCM(long[] nums)
{
    long ans = nums[0];
    for (int i = 1; i < nums.Length; i++)
    {
        ans = nums[i] * ans / GCD(nums[i], ans);
    }
    return ans;
}

static long GCD(long x, long y) { return y == 0 ? x : GCD(y, x % y); }

class Monkey
{
    public int Number { get; set; }
    public List<long> Items { get; set; }

    public Func<long, long> Operation { get; set; }
    public long DivisibleBy { get; set; }
    public int TrueThrowTo { get; set; }
    public int FalseThrowTo { get; set; }
    public long InspectCount { get; set; } = 0;

    public static long LCM = 0;
}