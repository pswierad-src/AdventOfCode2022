namespace Day14;

public static class Sand
{
    public static void AddSandToCave(Dictionary<(int x, int y), char> cave,
        (int x, int y) sandEntry)
    {
        var height = cave.Keys.Select(a => a.y).Max();
        
        (int x, int y) DropSand((int x, int y) sandLocation)
        {
            var currentSand = (sandLocation.x, sandLocation.y);
            
            if (currentSand.y > height)
            {
                //return (-1, -1);
                return (currentSand.x, currentSand.y);
            }

            if (!cave.ContainsKey((currentSand.x, currentSand.y + 1)))
            {
                return DropSand((currentSand.x, currentSand.y + 1));
            }

            if (!cave.ContainsKey((currentSand.x - 1, currentSand.y + 1)))
            {
                return DropSand((currentSand.x - 1, currentSand.y + 1));
            }

            if (!cave.ContainsKey((currentSand.x + 1, currentSand.y + 1)))
            {
                return DropSand((currentSand.x + 1, currentSand.y + 1));
            }

            return (currentSand.x, currentSand.y);
        }

        var nextDrop = DropSand(sandEntry);

        while (nextDrop != (-1, -1))
        {
            cave[nextDrop] = 'o';
            //Cave.DrawCave(cave);
            if (nextDrop == sandEntry)
            {
                break;
            }

            nextDrop = DropSand(sandEntry);
        }
    }
}