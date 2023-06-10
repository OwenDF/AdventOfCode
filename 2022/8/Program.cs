var input = await File.ReadAllLinesAsync("Input.txt");

var visibleTrees = 0;

var yLength = input.Length;
var xLength = input[0].Length;

var numGrid = new int[xLength, yLength];
var boolGrid = new bool[xLength, yLength];

for (var x = 0; x < xLength; x++)
for (var y = 0; y < yLength; y++)
{
    numGrid[x, y] = int.Parse(input[y][x].ToString());
}

for (var x = 0; x < xLength; x++)
{
    var tallestTree = -1;
    for (var y = 0; y < yLength; y++)
    {
        boolGrid[x, y] = boolGrid[x, y] || numGrid[x, y] > tallestTree;
        tallestTree = Math.Max(tallestTree, numGrid[x, y]);
    }
}

for (var x = 0; x < xLength; x++)
{
    var tallestTree = -1;
    for (var y = yLength - 1; y >= 0; y--)
    {
        boolGrid[x, y] = boolGrid[x, y] || numGrid[x, y] > tallestTree;
        tallestTree = Math.Max(tallestTree, numGrid[x, y]);
    }
}

for (var y = 0; y < yLength; y++)
{
    var tallestTree = -1;
    for (var x = 0; x < xLength; x++)
    {
        boolGrid[x, y] = boolGrid[x, y] || numGrid[x, y] > tallestTree;
        tallestTree = Math.Max(tallestTree, numGrid[x, y]);
    }
}

for (var y = 0; y < yLength; y++)
{
    var tallestTree = -1;
    for (var x = xLength - 1; x >= 0; x--)
    {
        boolGrid[x, y] = boolGrid[x, y] || numGrid[x, y] > tallestTree;
        tallestTree = Math.Max(tallestTree, numGrid[x, y]);

        if (boolGrid[x, y]) visibleTrees++;
    }
}

Console.WriteLine(visibleTrees);
