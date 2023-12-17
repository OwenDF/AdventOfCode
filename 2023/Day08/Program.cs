using static Day08.Functions;

var lines = File.ReadAllLines("Input.txt");

var sequence = lines[0].ToArray();
var nodes = lines.Skip(2).Select(ToNode).ToDictionary(x => x.Id, x => x);

var stepCounts = nodes.Keys.Where(x => x.EndsWith('A')).Select(x => GetStepCountsToNode(nodes, sequence, x));

Console.WriteLine(stepCounts.Select(x => (long)x).Aggregate(LCM));
