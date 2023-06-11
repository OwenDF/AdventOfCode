using _12;
using static _12.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");
var xLength = input[0].Length;
var yLength = input.Length;

var heights = new int[xLength, yLength];
var end = new Point();
var allNodes = new Dictionary<Point, int>();

for (var y = 0; y < input.Length; y++)
for (var x = 0; x < input[0].Length; x++)
{
    switch (input[y][x])
    {
        case 'S':
            // pointsToVisit.Add(new(new(x, y), 0));
            allNodes.Add(new(x, y), int.MaxValue);
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

var lowestPoints = new HashSet<Point>();

for (var y = 0; y < input.Length; y++)
for (var x = 0; x < input[0].Length; x++)
{
    if (heights[x, y] is 'a') lowestPoints.Add(new(x, y));
}

var minPath = int.MaxValue;

foreach (var startingPoint in lowestPoints)
{
    var allNodesCopy = new Dictionary<Point, int>(allNodes);
    var pointsToVisit = new HashSet<WeightedNode>();
    var visitedNodes = new HashSet<Point>();

    pointsToVisit.Add(new(startingPoint, 0));
    allNodesCopy[startingPoint] = 0;
    
    FindMinimumDistances(pointsToVisit, allNodesCopy, visitedNodes, heights, xLength, yLength);
    minPath = Math.Min(allNodesCopy[end], minPath);
}

Console.WriteLine(minPath);
 