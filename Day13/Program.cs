using Day13;
using Microsoft.VisualBasic;

var rawData = File.ReadAllText("input.txt");

var packetPairs = rawData.Split(Environment.NewLine + Environment.NewLine)
    .ToArray()
    .Select(p => p.Split(Environment.NewLine))
    .ToArray();

var orders = new List<ComparisionEntry>();

for (int i = 0; i < packetPairs.Length; i++)
{
    var left = ParseUtils.HandleList(ParseUtils.ParsePacketToList(packetPairs[i][0].Trim()));
    var right = ParseUtils.HandleList(ParseUtils.ParsePacketToList(packetPairs[i][1].Trim()));

    var eval = Comparator.CompareLists(left, right);

    if (!eval.HasValue) throw new Exception("Something is wrong");
    
    orders.Add(new ComparisionEntry(i+1, eval.Value));
}

Console.WriteLine($"Sum is {orders.Where(x => x.IsOrderRight).Sum(y => y.Index)}");

// Part 2
var p1 = "[[2]]";
var p2 = "[[6]]";

var packets = rawData.Split(Environment.NewLine).Where(x => x.Trim() != string.Empty);

var listBefore = new List<string>();
var listBetween = new List<string>();
var listAfter = new List<string>();



foreach (var packet in packets)
{
    var isPacketSmallerThan2 = Comparator.CompareLists(
        ParseUtils.HandleList(ParseUtils.ParsePacketToList(packet.Trim())),
        ParseUtils.HandleList(ParseUtils.ParsePacketToList(p1.Trim())));

    if (isPacketSmallerThan2.Value)
    {
        listBefore.Add(packet);
        continue;
    }

    var isPacketSmallerThan6 = Comparator.CompareLists(
        ParseUtils.HandleList(ParseUtils.ParsePacketToList(packet.Trim())),
        ParseUtils.HandleList(ParseUtils.ParsePacketToList(p2.Trim())));

    if (isPacketSmallerThan2.Value == false && isPacketSmallerThan6.Value)
    {
        listBetween.Add(packet);
        continue;
    }

    if (isPacketSmallerThan6.Value == false)
    {
        listAfter.Add(packet);
        continue;
    }

    throw new Exception("Error");
}

var quasiOrderedList = new List<string>();

quasiOrderedList.AddRange(listBefore);
quasiOrderedList.Add(p1);
quasiOrderedList.AddRange(listBetween);
quasiOrderedList.Add(p2);
quasiOrderedList.AddRange(listAfter);

var a = quasiOrderedList.IndexOf(p1) + 1;
var b = quasiOrderedList.IndexOf(p2) + 1;

Console.WriteLine($"Decoder key is {a*b}");

Console.ReadLine();


record ComparisionEntry(int Index, bool IsOrderRight);
//record Packet(int Index, string Data);