using static Day08.Functions;

var lines = File.ReadAllLines("Input.txt");

var sequence = lines[0];
var nodes = lines.Skip(2).Select(ToNode).ToDictionary(x => x.Id, x => x);

Console.WriteLine(GetStepCountsToNode(nodes, sequence.ToArray(), "AAA", "ZZZ"));