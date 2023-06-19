using _22;
using static _22.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");

var maxY = input.Length - 2;
var maxX = 0;
var openSpaces = new HashSet<Point>();
var walls = new HashSet<Point>();
for (var y = 0; y < maxY; y++)
{
    maxX = Math.Max(maxX, input[y].Length);
    for (var x = 0; x < input[y].Length; x++) 
    {
        switch (input[y][x])
        {
            case '.':
                openSpaces.Add(new(x, y));
                break;
            case '#':
                walls.Add(new(x, y));
                break;
        }
    }
}

var map = new Map(openSpaces, walls, maxX, maxY);
var instructions = ParseInstructions(input.Last());

var startLocation = new Point(0,0);

while (!openSpaces.Contains(startLocation))
{
    startLocation = startLocation with { X = startLocation.X + 1 };
}

var endPosition = instructions.Aggregate(new Position('E', startLocation), (c, n) => Move(c, n, map));

Console.WriteLine(endPosition);

Console.WriteLine((endPosition.Location.Y + 1) * 1000 + (endPosition.Location.X + 1) * 4 + endPosition.Direction switch
{
    'E' => 0,
    'S' => 1,
    'W' => 2,
    'N' => 3
});