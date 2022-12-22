using System.Text;
using Day14;

var rawData = File.ReadAllLines("input.txt");

var mappedCave = Cave.ScanCave(rawData);

Sand.AddSandToCave(mappedCave, (500, 0));

Console.WriteLine(mappedCave.Values.Count(v => v == 'o'));

Console.ReadLine();



