var map = AsTwoDimensionalArray(File.ReadAllLines("Input.txt"));

var visitedLocations = new HashSet<(int, int)>();
var startingLocation = FindStartingLocation(map);

RunRoute(map, startingLocation, visitedLocations);

Console.WriteLine(visitedLocations.Count);

void RunRoute(char[,] map, (int x, int y) startingLocation, HashSet<(int, int)> visitedLocations)
{
    // turn right from west = north
    var currentDir = TurnRight((-1, 0));
    var currentPosition = startingLocation;

    while (true)
    {
        visitedLocations.Add(currentPosition);
        (int x, int y) nextPosition = (currentPosition.x + currentDir.x, currentPosition.y + currentDir.y);
        
        if (nextPosition.x < 0 || nextPosition.x > map.GetUpperBound(0) ||
            nextPosition.y < 0 || nextPosition.y > map.GetUpperBound(1)) 
            return;

        if (map[nextPosition.x, nextPosition.y] is '#')
        {
            currentDir = TurnRight(currentDir);
            continue;
        }

        currentPosition = nextPosition;
    }
}

(int x, int y) TurnRight((int, int) currentDir) => currentDir switch
{
    (0, -1) => (1, 0),
    (1, 0) => (0, 1),
    (0, 1) => (-1, 0),
    (-1, 0) => (0, -1)
};


    (int, int) FindStartingLocation(char[,] map)
{
    for (var i = 0; i < map.GetUpperBound(1); i++)
    for (var j = 0; j < map.GetUpperBound(0); j++)
        if (map[i, j] is '^')
            return (i, j);

    throw new Exception();
}

char[,] AsTwoDimensionalArray(string[] lines)
{
    var result = new char[lines.Length, lines[0].Length];
    for (var i = 0; i < lines[0].Length; i++)
    for (var j = 0; j < lines.Length; j++)
        result[i, j] = lines[j][i];

    return result;
}