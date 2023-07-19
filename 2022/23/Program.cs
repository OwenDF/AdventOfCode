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

var counter = 0;
while (true)
{
    bool finished;
    (finished, elves) = DisperseElves(elves, directions[counter % 4]);
    counter++;

    if (finished) break;
}

Console.WriteLine(counter);