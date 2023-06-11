using _12;
using static _12.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");
var xLength = input[0].Length;
var yLength = input.Length;

var pointsToVisit = new HashSet<WeightedNode>();
var heights = new int[xLength, yLength];
var end = new Point();
var visitedNodes = new HashSet<Point>();
var allNodes = new Dictionary<Point, int>();

for (var y = 0; y < input.Length; y++)
for (var x = 0; x < input[0].Length; x++)
{
    switch (input[y][x])
    {
        case 'S':
            pointsToVisit.Add(new(new(x, y), 0));
            allNodes.Add(new(x, y), 0);
            heights[x, y] = 'a';
            break;
        case 'E':
            end = new(x, y);
            allNodes.Add(new(x, y), int.MaxValue);
            heights[x, y] = 'z';
            break;
        default:
            allNodes.Add(new(x, y), int.MaxValue);
            heights[x, y] = input[y][x];
            break;
    }
}

FindMinimumDistances(pointsToVisit, allNodes, visitedNodes, heights, xLength, yLength);

for (var y = 0; y < input.Length; y++)
{
    for (var x = 0; x < input[0].Length; x++) 
    { 
        Console.Write(allNodes[new(x,y)].ToString("00") + "\t");
    }

    Console.WriteLine();
}

Console.WriteLine(allNodes[end]);
 