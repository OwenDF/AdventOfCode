using Day03;
using static Day03.Functions;

var lines = File.ReadAllLines("Input.txt");

var width = lines[0].Length;
var grid = new char[width, lines.Length];

for (var i = 0; i < width; i++) for (var j = 0; j < lines.Length; j++) grid[i, j] = lines[j][i];

var numbers = new Dictionary<Point, Box<int>>();

for (var y = 0; y < lines.Length; y++)
{
    Box<int>? partNum = null;
    for (var x = 0; x < width; x++)
    {
        if (!char.IsDigit(grid[x, y]))
        {
            partNum = null;
            continue;
        }

        if (partNum is null) partNum = new Box<int>(0);

        partNum.Value = partNum.Value * 10 + grid[x, y].ToNum();
        numbers[new(x, y)] = partNum;
    }
}

var sum = 0;

for (var x = 0; x < width; x++) for (var y = 0; y < lines.Length; y++)
{
    if (grid[x, y] is not '*') continue;

    var adjacentNumbers = GetAdjacentNumbers(new(x, y), numbers);
    if (adjacentNumbers.Length is 2) sum += adjacentNumbers[0] * adjacentNumbers[1];
}

Console.WriteLine(sum);
