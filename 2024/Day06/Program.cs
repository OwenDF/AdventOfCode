var map = AsTwoDimensionalArray(File.ReadAllLines("Input.txt"));

var loopPositions = 0;
var startingLocation = FindStartingLocation(map);

var defaultRoute = GetDefaultRoute(map, startingLocation);
defaultRoute.Remove(startingLocation);

foreach(var (i,j) in defaultRoute)  
{
    if (map[i, j] is '^' or '#') continue;

    map[i, j] = '#';
    if (IsRouteCircular(map, startingLocation)) loopPositions++;
    map[i, j] = 'a';
}

Console.WriteLine(loopPositions);

HashSet<(int, int)> GetDefaultRoute(char[,] map, (int x, int y) startingLocation)
{
    // turn right from west = north
    var currentDir = TurnRight((-1, 0));
    var currentPosition = startingLocation;
    var visitedPositions = new HashSet<(int, int)>();

    while (true)
    {
        visitedPositions.Add(currentPosition);
        (int x, int y) nextPosition = (currentPosition.x + currentDir.x, currentPosition.y + currentDir.y);
        
        if (nextPosition.x < 0 || nextPosition.x > map.GetUpperBound(0) ||
            nextPosition.y < 0 || nextPosition.y > map.GetUpperBound(1)) 
            return visitedPositions;

        if (map[nextPosition.x, nextPosition.y] is '#')
        {
            currentDir = TurnRight(currentDir);
            continue;
        }

        currentPosition = nextPosition;
    }
}

bool IsRouteCircular(char[,] map, (int x, int y) startingLocation)
{
    // turn right from west = north
    var startingDirection = TurnRight((-1, 0));
    var currentDir = startingDirection;
    var currentPosition = startingLocation;
    var started = false;
    var route = new HashSet<((int, int), (int, int))>();

    while (true)
    {
        (int x, int y) nextPosition = (currentPosition.x + currentDir.x, currentPosition.y + currentDir.y);
        
        if (nextPosition.x < 0 || nextPosition.x > map.GetUpperBound(0) ||
            nextPosition.y < 0 || nextPosition.y > map.GetUpperBound(1)) 
            return false;

        if (map[nextPosition.x, nextPosition.y] is '#')
        {
            currentDir = TurnRight(currentDir);
            continue;
        }

        if (!route.Add((currentPosition, currentDir))) return true;
        
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