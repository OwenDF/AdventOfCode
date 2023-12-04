using Day03;

var lines = File.ReadAllLines("Input.txt");

var width = lines[0].Length;
var grid = new char[width, lines.Length];

for (var i = 0; i < width; i++) for (var j = 0; j < lines.Length; j++) grid[i, j] = lines[j][i];

var sum = 0;

for (var y = 0; y < lines.Length; y++)
{
    var partNum = 0;
    var isPartNum = false;
    for (var x = 0; x < width; x++)
    {
        if (!char.IsDigit(grid[x, y]))
        {
            if (isPartNum) sum += partNum;
            isPartNum = false;
            partNum = 0;
            continue;
        }

        isPartNum = isPartNum || new Point(x, y).IsPartNumber(grid, width - 1, lines.Length - 1);
        partNum *= 10;
        partNum += int.Parse(grid[x, y].ToString());
    }

    if (isPartNum) sum += partNum;
}

Console.WriteLine(sum);