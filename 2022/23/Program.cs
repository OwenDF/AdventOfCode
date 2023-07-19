using static _23.Functions;
using static _23.Direction;

var input = await File.ReadAllLinesAsync("Input.txt");

var elves = ParseInitialElfPositions(input);

var directions = new[]
{
    new[] { N, S, W, E },
    new[] { S, W, E, N },
    new[] { W, E, N, S },
    new[] { E, N, S, W }
};

for (var i = 0; i < 10; i++)
    elves = DisperseElves(elves, directions[i % 4]);

var maxX = elves.Select(e => e.X).Max();
var minX = elves.Select(e => e.X).Min();
var maxY = elves.Select(e => e.Y).Max();
var minY = elves.Select(e => e.Y).Min();

var gridSize = (maxX - minX + 1) * (maxY - minY + 1);

Console.WriteLine(gridSize - elves.Count);

// for (var i = minY; i <= maxY; i++)
// {
//     for (var j = minX; j <= maxX; j++)
//     {
//         if (elves.Contains(new(j, i))) Console.Write("#");
//         else Console.Write(".");
//     }
//
//     Console.WriteLine();
// }
