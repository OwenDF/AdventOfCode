using Day10;
using static Day10.Functions;
using static Day10.Direction;

var lines = File.ReadAllLines("Input.txt");
var yLength = lines.Length;
var xLength = lines[0].Length;

var grid = new Terrain[xLength,yLength];
var start = new Position(-1, -1);

for (var y = 0; y < yLength; y++) for (var x = 0; x < xLength; x++)
{
    var terrain = FromCharacter(lines[y][x]);
    if (terrain is Terrain.Start) start = new Position(x, y);
    grid[x, y] = terrain;
}

var counts = new[]
{
    (start with { X = start.X - 1 }, East),
    (start with { X = start.X + 1 }, West),
    (start with { Y = start.Y - 1 }, South),
    (start with { Y = start.Y + 1 }, North)
}.Select(x => GetCountBackToStart(grid, x.Item1, 1, x.Item2)).Where(x => x is not null).Cast<int>().ToList();

if (counts.Count != 2 || counts[0] != counts[1]) throw new Exception();

Console.WriteLine(counts[0]/2);