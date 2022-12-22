using System.Text;

namespace Day14;

public static class Cave
{
    public static void DrawCave(Dictionary<(int x, int y), char> cave)
    {
        Console.Clear();
        
        var minX = cave.Keys.Select(k => k.x).Min();
        var maxX = cave.Keys.Select(k => k.x).Max();
        var minY = cave.Keys.Select(k => k.y).Min();
        var maxY = cave.Keys.Select(k => k.y).Max();

        var map = new StringBuilder();
        for (var y = minY; y <= maxY; y++)
        {
            var line = new StringBuilder();
            for (var x = minX; x <= maxX; x++)
            {
                if (cave.ContainsKey((x, y)))
                {
                    line.Append(cave[(x, y)]);
                }
                else
                {
                    line.Append('.');
                }
            }
            map.AppendLine(line.ToString());
        }
        Console.Write(map.ToString());
        Thread.Sleep(100);
    }

    public static Dictionary<(int x, int y), char> ScanCave(string[] input)
    {
        var cave = new Dictionary<(int x, int y), char>();

        foreach (var line in input)
        {
            var elements = line.Split(" -> ")
                .Select(l => l.Split(',')
                    .Select(int.Parse)
                    .ToList())
                .Select(p => (p.First(), p.Last()))
                .ToList();

            for (int i = 0; i < elements.Count - 1; i++)
            {
                cave.FillLine(elements[i], elements[i+1]);
            }
        }

        return cave;
    }

    private static void FillLine(this IDictionary<(int x, int y), char> cave,
        (int x, int y) start,
        (int x, int y) end)
    {
        var minX = Math.Min(start.x, end.x);
        var maxX = Math.Max(start.x, end.x);
        var minY = Math.Min(start.y, end.y);
        var maxY = Math.Max(start.y, end.y);

        for (var y = minY; y <= maxY; y++)
        {
            for (var x = minX; x <= maxX; x++)
            {
                cave[(x, y)] = '#';
            }
        }
    }
}