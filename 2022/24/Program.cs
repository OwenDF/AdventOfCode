using _24;
using static _24.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");

var maxX = input[0].Length - 2;
var maxY = input.Length - 2;

var entrance = new Point(1, 0);
var exit = new Point(maxX, maxY + 1);

var blizzardState = ReadInitialBlizzardState(input);
var possibleLocations = new HashSet<Point> { entrance };

var counter = 0;
while (true)
{
    blizzardState = blizzardState.Progress(maxX, maxY);
    var state = blizzardState;
    possibleLocations = possibleLocations
        .SelectMany(l => l.AdjacentPoints.Where(ap => ap.IsSafePoint(state, entrance, exit, maxX, maxY)))
        .Concat(possibleLocations.Where(p => p.IsSafePoint(state, entrance, exit, maxX, maxY)))
        .ToHashSet();

    counter++;

    if (possibleLocations.Contains(exit)) break;
}

Console.WriteLine(counter);