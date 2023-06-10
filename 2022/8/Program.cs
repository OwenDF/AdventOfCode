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

var bestTreeScore = 0;

for (var x = 1; x < xLength; x++)
for (var y = 1; y < yLength; y++)
{
    var treeHeight = numGrid[x, y];
    var xPlusScore = 0;
    for (var xPlus = x + 1; xPlus < xLength; xPlus++)
    {
        xPlusScore++;
        if (numGrid[xPlus, y] >= treeHeight) break;
    }
    
    var xMinusScore = 0;
    for (var xMinus = x - 1; xMinus >= 0; xMinus--)
    {
        xMinusScore++;
        if (numGrid[xMinus, y] >= treeHeight) break;
    }
    
    var yPlusScore = 0;
    for (var yPlus = y + 1; yPlus < yLength; yPlus++)
    {
        yPlusScore++;
        if (numGrid[x, yPlus] >= treeHeight) break;
    }
    
    var yMinusScore = 0;
    for (var yMinus = y - 1; yMinus >= 0; yMinus--)
    {
        yMinusScore++;
        if (numGrid[x, yMinus] >= treeHeight) break;
    }

    bestTreeScore = Math.Max(bestTreeScore, xPlusScore * xMinusScore * yPlusScore * yMinusScore);
}

Console.WriteLine(bestTreeScore);
