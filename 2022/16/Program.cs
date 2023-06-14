using _16;
using static _16.Functions;

var valves = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.MapToValve()).ToDictionary(x => x.Name, x => x);

var valvesToVisit = valves.Where(x => x.Value.FlowRate is not 0).Select(x => x.Value).Append(valves["AA"]).ToList();

var valveDistances = new Dictionary<ValvePair, int>();
foreach(var valve1 in valvesToVisit)
foreach (var valve2 in valvesToVisit)
{
    var valvePair = new ValvePair(valve1, valve2);
    if (valve1 == valve2 || valveDistances.ContainsKey(valvePair)) continue;

    valveDistances.Add(valvePair, FindValveDistance(valvePair, valves));
}

Console.WriteLine("Distances calculated");

var data = valveDistances.Select(x => new ValvePairDistance(x.Key, x.Value)).ToList();

Console.WriteLine();
Console.WriteLine(FindBestPressureReleaseFor2(data));

Console.WriteLine(Counter);