using _22;
using static _22.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");

var north = CreateFace(input.Take(50).Select(x => x.Skip(50).Take(50).ToArray()).ToArray());
var east = CreateFace(input.Take(50).Select(x => x.Skip(100).Take(50).ToArray()).ToArray());
var bottom = CreateFace(input.Skip(50).Take(50).Select(x => x.Skip(50).Take(50).ToArray()).ToArray());
var west = CreateFace(input.Skip(100).Take(50).Select(x => x.Take(50).ToArray()).ToArray());
var south = CreateFace(input.Skip(100).Take(50).Select(x => x.Skip(50).Take(50).ToArray()).ToArray());
var top = CreateFace(input.Skip(150).Take(50).Select(x => x.Take(50).ToArray()).ToArray());

var cube = new Cube(bottom, top, north, south, west, east);
var start = new Position('E', new Point(0, 0), FaceId.North);
var instructions = ParseInstructions(input.Last());

var endPosition = instructions.Aggregate(start, (c, n) => Move(c, n, cube));

Console.WriteLine(endPosition);

var endX = endPosition.Location.X + 1;
var endY = endPosition.Location.Y + 1;

if (endPosition.Face is FaceId.North or FaceId.Bottom or FaceId.South) endX += 50;
if (endPosition.Face is FaceId.East) endX += 100;
if (endPosition.Face is FaceId.Bottom) endY += 50;
if (endPosition.Face is FaceId.West or FaceId.South) endY += 100;
if (endPosition.Face is FaceId.Top) endY += 150;


Console.WriteLine(endY * 1000 + endX * 4 + endPosition.Direction switch
{
    'E' => 0,
    'S' => 1,
    'W' => 2,
    'N' => 3
});