using _15;
using static _15.Functions;

const int y = 2_000_000;
var pairs = (await File.ReadAllLinesAsync("Input.txt")).Select(x => x.MapSensorBeaconPair()).ToList();
var sensors = pairs.Select(x => new Sensor(x.SensorLocation, GetDistance(x.SensorLocation, x.BeaconLocation)))
    .ToHashSet();
var beacons = pairs.Select(x => x.BeaconLocation).ToHashSet();

var minX = sensors.Select(x => x.Location.X - x.Range).Min();
var maxX = sensors.Select(x => x.Location.X + x.Range).Max();

var coveredLocations = 0;

// for (var x = minX; x <= maxX; x++)
// {
//     if (sensors.Any(sensor => GetDistance(sensor.Location, new Point(x, y)) <= sensor.Range))
//     {
//         coveredLocations++;
//     }
// }

var beaconsInRow = beacons.Count(x => x.Y is y);

Console.WriteLine(coveredLocations - beaconsInRow);

var beacon = FindUncoveredPoint(pairs);

Console.WriteLine((beacon.X * 4_000_000L) + beacon.Y);