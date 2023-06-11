using _9;
using static _9.Functions;

var instructions = (await File.ReadAllLinesAsync("Input.txt")).Select(MapToInstruction);

var tailVisits = new HashSet<Point>();
var knots = new Point[10];

foreach (var instruction in instructions)
{
    MoveKnots(instruction, knots, tailVisits);
}

Console.WriteLine(tailVisits.Count);