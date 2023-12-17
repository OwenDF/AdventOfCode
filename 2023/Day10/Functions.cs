using static Day10.Direction;
using static Day10.Terrain;

namespace Day10;

internal static class Functions
{
    public static Terrain FromCharacter(char c) => c switch
    {
        '|' => NorthSouth,
        '-' => EastWest,
        'L' => NorthEast,
        'J' => NorthWest,
        '7' => SouthWest,
        'F' => SouthEast,
        '.' => Ground,
        'S' => Start
    };
    
    public static int? GetCountBackToStart(
        Terrain[,] terrainGrid,
        Position position,
        int currentCount,
        Direction comingFrom)
    {
        if (!IsValidPointInGrid(terrainGrid, position)) return null;
        
        var thisTerrain = terrainGrid[position.X, position.Y];
        if (!TerrainIsValid(thisTerrain, comingFrom)) return null;
        if (thisTerrain is Start) return ++currentCount;
        var exit = GetNextDirection(thisTerrain, comingFrom);
        var nextPosition = GetNextPosition(position, exit);
        
        if (!IsValidPointInGrid(terrainGrid, nextPosition)) return null;
        
        return TerrainIsValid(terrainGrid[nextPosition.X, nextPosition.Y], exit.Invert()) ?
            GetCountBackToStart(terrainGrid, nextPosition, ++currentCount, exit.Invert()) :
            null;
    }

    private static bool TerrainIsValid(Terrain terrain, Direction border) => terrain switch
    {
        NorthSouth => border is North or South,
        EastWest => border is East or West,
        NorthWest => border is North or West,
        NorthEast => border is North or East,
        SouthWest => border is South or West,
        SouthEast => border is South or East,
        Start => true,
        _ => false
    };
    
    private static Direction GetNextDirection(Terrain terrain, Direction comingFrom) => (terrain, comingFrom) switch
    {
        (NorthSouth, North) => South,
        (NorthSouth, South) => North,
        (EastWest, East) => West,
        (EastWest, West) => East,
        (NorthWest, North) => West,
        (NorthWest, West) => North,
        (NorthEast, North) => East,
        (NorthEast, East) => North,
        (SouthEast, South) => East,
        (SouthEast, East) => South,
        (SouthWest, South) => West,
        (SouthWest, West) => South
    };

    private static Position GetNextPosition(Position current, Direction direction) => direction switch
    {
        North => current with { Y = current.Y - 1 },
        South => current with { Y = current.Y + 1 },
        West => current with { X = current.X - 1 },
        East => current with { X = current.X + 1 }
    };

    private static Direction Invert(this Direction direction) => direction switch
    {
        North => South, South => North, West => East, East => West
    };

    private static bool IsValidPointInGrid(Terrain[,] grid, Position point)
        => !(point.X < 0 || point.Y < 0 || point.X > grid.GetUpperBound(0) || point.Y > grid.GetUpperBound(1));
}

public record struct Position(int X, int Y);