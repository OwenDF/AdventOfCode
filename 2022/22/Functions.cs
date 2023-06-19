namespace _22;

public static class Functions
{
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

    public static Position Move(Position currentPosition, Instruction instruction, Map map) => instruction switch
    {
        TurnInstruction ti => ti.Direction switch
        {
            'R' => currentPosition with { Direction = TurnRight(currentPosition.Direction)},
            'L' => currentPosition with { Direction = TurnLeft(currentPosition.Direction)}
        },
        MoveInstruction mi => currentPosition with { Location = Move(currentPosition, mi.Count, map) }
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

    private static Point Move(Position current, int moveCount, Map map)
    {
        for (var i = 0; i < moveCount; i++)
        {
            var next = current.Direction switch 
            {
                'N' => MoveNorth(current.Location, map),
                'E' => MoveEast(current.Location, map),
                'S' => MoveSouth(current.Location, map),
                'W' => MoveWest(current.Location, map)
            };
            
            if (next is null)
            {
                return current.Location;
            }

            current = current with { Location = next.Value };
        }

        return current.Location;
    }

    private static Point? MoveNorth(Point current, Map map)
    {
        var next = current with { Y = current.Y - 1 };

        if (map.Walls.Contains(next)) return null;
        if (map.OpenSpaces.Contains(next)) return next;

        next = next.Y < 0 ? next with { Y = map.MaxY } : next;
        return MoveNorth(next, map);
    }
    
    private static Point? MoveSouth(Point current, Map map)
    {
        var next = current with { Y = current.Y + 1 };

        if (map.Walls.Contains(next)) return null;
        if (map.OpenSpaces.Contains(next)) return next;

        next = next.Y >= map.MaxY ? next with { Y = - 1 } : next;
        return MoveSouth(next, map);
    }
    
    private static Point? MoveEast(Point current, Map map)
    {
        var next = current with { X = current.X + 1 };

        if (map.Walls.Contains(next)) return null;
        if (map.OpenSpaces.Contains(next)) return next;

        next = next.X >= map.MaxX ? next with { X = - 1 } : next;
        return MoveEast(next, map);
    }
    
    private static Point? MoveWest(Point current, Map map)
    {
        var next = current with { X = current.X - 1 };

        if (map.Walls.Contains(next)) return null;
        if (map.OpenSpaces.Contains(next)) return next;

        next = next.X < 0 ? next with { X = map.MaxX } : next;
        return MoveWest(next, map);
    }
}