using _18;
using static _18.Functions;

var points = (await File.ReadAllLinesAsync("Input.txt")).Select(ToPoint).ToHashSet();

var maxX = points.Max(x => x.X);
var maxY = points.Max(x => x.Y);
var maxZ = points.Max(x => x.Z);
var minX = points.Min(x => x.X);
var minY = points.Min(x => x.Y);
var minZ = points.Min(x => x.Z);

var pointsCopy = new HashSet<Point>(points);

foreach (var point in pointsCopy)
foreach (var adjacentPoint in point.GetAllAdjacentPoints())
{
    points = FloodFill(points, adjacentPoint, new(minX, minY, minZ), new(maxX, maxY, maxZ));
}

var surfaceArea = points.Count * 6;

foreach (var point in points)
{
    if (points.Contains(point with { X = point.X - 1 })) surfaceArea -= 1;
    if (points.Contains(point with { X = point.X + 1 })) surfaceArea -= 1;
    if (points.Contains(point with { Y = point.Y - 1 })) surfaceArea -= 1;
    if (points.Contains(point with { Y = point.Y + 1 })) surfaceArea -= 1;
    if (points.Contains(point with { Z = point.Z - 1 })) surfaceArea -= 1;
    if (points.Contains(point with { Z = point.Z + 1 })) surfaceArea -= 1;
}

Console.WriteLine(surfaceArea);