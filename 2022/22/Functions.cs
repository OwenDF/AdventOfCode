namespace _22;

using static FaceId;

public static class Functions
{
    public static Face CreateFace(char[][] grid)
    {
        var walls = new HashSet<Point>();
        for (var i = 0; i < 50; i++)
        for (var j = 0; j < 50; j++)
        {
            switch (grid[j][i])
            {
                case '#':
                    walls.Add(new(i, j));
                    break;
                case '.':
                    continue;
                default:
                    throw new Exception();
            }
        }

        return new(walls);
    }

    public static IReadOnlyList<Instruction> ParseInstructions(string input)
    {
        var instructions = new List<Instruction>();
        var number = "";

        foreach (var character in input)
        {
            if (char.IsDigit(character)) number += character;

            if (char.IsLetter(character))
            {
                instructions.Add(new MoveInstruction(int.Parse(number)));
                number = "";
                instructions.Add(new TurnInstruction(character));
            }
        }

        instructions.Add(new MoveInstruction(int.Parse(number)));

        return instructions;
    }

    public static Position Move(Position currentPosition, Instruction instruction, Cube cube) => instruction switch
    {
        TurnInstruction ti => ti.Direction switch
        {
            'R' => currentPosition with { Direction = TurnRight(currentPosition.Direction)},
            'L' => currentPosition with { Direction = TurnLeft(currentPosition.Direction)}
        },
        MoveInstruction mi => Move(currentPosition, mi.Count, cube)
    };

    private static char TurnRight(char currentDirection) => currentDirection switch
    {
        'E' => 'S',
        'S' => 'W',
        'W' => 'N',
        'N' => 'E'
    };
    
    private static char TurnLeft(char currentDirection) => currentDirection switch
    {
        'E' => 'N',
        'S' => 'E',
        'W' => 'S',
        'N' => 'W'
    };

    private static Position Move(Position current, int moveCount, Cube cube)
    {
        for (var i = 0; i < moveCount; i++)
        {
            var next = current.Direction switch 
            {
                'N' => MoveNorth(current, cube),
                'E' => MoveEast(current, cube),
                'S' => MoveSouth(current, cube),
                'W' => MoveWest(current, cube)
            };
            
            if (next is null)
            {
                break;
            }

            current = next;
        }
    
        return current;
    }

    private static Func<Point, Position> GetTransitionFunction(FaceId start, FaceId end) => (start, end) switch
    {
        (Bottom, North) => p => new('N', p with { Y = 49 }, end),
        (Bottom, South) => p => new('S', p with { Y = 0 }, end),
        (Bottom, West) => p => new ('S', new(p.Y, 0), end),
        (Bottom, East) => p => new('N', new(p.Y, 49), end),
        (North, Bottom) => p => new('S', p with { Y = 0 }, end),
        (North, East) => p => new('E', p with { X = 0 }, end),
        (North, West) => p => new('E', new(0, 49 - p.Y), end),
        (North, Top) => p => new('E', new(0, p.X), end),
        (East, Bottom) => p => new('W', new(49, p.X), end),
        (East, North) => p => new('W', p with { X = 49 }, end),
        (East, South) => p => new('W', new(49, 49 - p.Y ), end),
        (East, Top) => p => new('N', p with { Y = 49 }, end),
        (South, Bottom) => p => new('N', p with { Y = 49 }, end),
        (South, West) => p => new('W', p with { X = 49 }, end),
        (South, East) => p => new('W', new(49, 49 - p.Y), end),
        (South, Top) => p => new('W', new(49, p.X), end),
        (West, Bottom) => p => new('E', new(0, p.X), end),
        (West, South) => p => new('E', p with { X = 0 }, end),
        (West, Top) => p => new('S', p with { Y = 0 }, end),
        (West, North) => p => new('E', new(0, 49 - p.Y), end),
        (Top, West) => p => new('N', p with { Y = 49 }, end),
        (Top, South) => p => new('N', new(p.Y, 49), end),
        (Top, North) => p => new('S', new(p.Y, 0), end),
        (Top, East) => p => new('S', p with { Y = 0 }, end)
    };

    private static FaceId GetNorthAdjacentFace(FaceId currentFace) => currentFace switch
    {
        Bottom => North,
        North => Top,
        East => Top,
        South => Bottom,
        West => Bottom,
        Top => West
    };
    
    private static FaceId GetSouthAdjacentFace(FaceId currentFace) => currentFace switch
    {
        Bottom => South,
        North => Bottom,
        East => Bottom,
        South => Top,
        West => Top,
        Top => East
    };
    
    private static FaceId GetEastAdjacentFace(FaceId currentFace) => currentFace switch
    {
        Bottom => East,
        North => East,
        East => South,
        South => East,
        West => South,
        Top => South
    };
    
    private static FaceId GetWestAdjacentFace(FaceId currentFace) => currentFace switch
    {
        Bottom => West,
        North => West,
        East => North,
        South => West,
        West => North,
        Top => North
    };

    private static bool IsFreePosition(Position position, Cube cube) => !(position.Face switch
    {
        Bottom => cube.Bottom,
        Top => cube.Top,
        North => cube.North,
        South => cube.South,
        West => cube.West,
        East => cube.East
    }).Walls.Contains(position.Location);

    private static Position? MoveToOtherFace(Position currentPosition, FaceId newFace, Cube cube)
    {
        var newPosition = GetTransitionFunction(currentPosition.Face, newFace)(currentPosition.Location);
        return IsFreePosition(newPosition, cube) ? newPosition : null;
    }

    private static Position? MoveNorth(Position current, Cube cube)
    {
        if (current.Location.Y is 0) return MoveToOtherFace(current, GetNorthAdjacentFace(current.Face), cube);

        var newPosition = current with { Location = current.Location with { Y = current.Location.Y - 1 } };
        return IsFreePosition(newPosition, cube) ? newPosition : null;
    }
    
    private static Position? MoveSouth(Position current, Cube cube)
    {
        if (current.Location.Y is 49) return MoveToOtherFace(current, GetSouthAdjacentFace(current.Face), cube);

        var newPosition = current with { Location = current.Location with { Y = current.Location.Y + 1 } };
        return IsFreePosition(newPosition, cube) ? newPosition : null;
    }
    
    private static Position? MoveEast(Position current, Cube cube)
    {
        if (current.Location.X is 49) return MoveToOtherFace(current, GetEastAdjacentFace(current.Face), cube);

        var newPosition = current with { Location = current.Location with { X = current.Location.X + 1 } };
        return IsFreePosition(newPosition, cube) ? newPosition : null;
    }
    
    private static Position? MoveWest(Position current, Cube cube)
    {
        if (current.Location.X is 0) return MoveToOtherFace(current, GetWestAdjacentFace(current.Face), cube);

        var newPosition = current with { Location = current.Location with { X = current.Location.X - 1 } };
        return IsFreePosition(newPosition, cube) ? newPosition : null;
    }
}