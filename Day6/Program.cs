var rawData = File.ReadAllText("input.txt");

var startOfPacket = 4;
var startOfMessage = 14;

for (int i = 0; i < rawData.Length - startOfPacket; i++)
{
    var potentialMarker = rawData.Substring(i, startOfPacket);

    var charsInMarker = potentialMarker.ToList();

    if (charsInMarker.Count == charsInMarker.Distinct().Count())
    {
        Console.WriteLine($"{i+startOfPacket} characters needs to be processed before start of packet marker.");
        break;
    }
}

for (int i = 0; i < rawData.Length - startOfMessage; i++)
{
    var potentialMarker = rawData.Substring(i, startOfMessage);

    var charsInMarker = potentialMarker.ToList();

    if (charsInMarker.Count == charsInMarker.Distinct().Count())
    {
        Console.WriteLine($"{i+startOfMessage} characters needs to be processed before start of message marker.");
        break;
    }
}

Console.ReadLine();