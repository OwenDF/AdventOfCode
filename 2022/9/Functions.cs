namespace _9;

public static class Functions
{
    const char Up = 'U';
    const char Down = 'D';
    const char Right = 'R';
    const char Left = 'L';
    
    public static Instruction MapToInstruction(string input)
    {
        var split = input.Split(' ');
        return new Instruction(split[0][0], int.Parse(split[1]));
    }

    public static bool IsAdjacentTo(this Point first, Point second) =>
        first.IsXAdjacentTo(second) && first.IsYAdjacentTo(second);

    public static bool IsXAdjacentTo(this Point first, Point second) =>
        first.X - second.X is 0 or 1 or -1;

    public static bool IsYAdjacentTo(this Point first, Point second) =>
        first.Y - second.Y is 0 or 1 or -1;

    public static void MoveKnots(Instruction instruction, Point[] points, HashSet<Point> tailVisits)
    {
        var head = points[0];

        for (var i = 0; i < instruction.MoveCount; i++)
        {
            switch (instruction.Direction)
            {
                case Up:
                    head = head with { Y = head.Y + 1 };
                    break;
                case Down:
                    head = head with { Y = head.Y - 1 };
                    break;
                case Left:
                    head = head with { X = head.X - 1 };
                    break;
                case Right:
                    head = head with { X = head.X + 1 };
                    break;
            }
            points[0] = head;
            var tailPosition = MoveTailKnots(points);
            tailVisits.Add(tailPosition);
        }
    }
    
    private static Point MoveTailKnots(Span<Point> points)
    {
        if (points.Length is 1) return points[0];

        var head = points[0];
        var tail = points[1];
        
        if (tail.IsAdjacentTo(head)) goto end;
        
        if (tail.X == head.X)
        {
            if (tail.Y < head.Y)
            {
                tail = tail with { Y = tail.Y + 1 };
                goto end;
            }
        
            tail = tail with { Y = tail.Y - 1 };
            goto end;
        }
        
        if (tail.Y == head.Y)
        {
            if (tail.X < head.X)
            {
                tail = tail with { X = tail.X + 1 };
                goto end;
            }
        
            tail = tail with { X = tail.X - 1 };
            goto end;
        }
        
        if (tail.Y < head.Y && tail.X < head.X)
        {
            tail = tail with { X = tail.X + 1, Y = tail.Y + 1 };
            goto end;
        }
        
        if (tail.Y < head.Y && tail.X > head.X)
        {
            tail = tail with { X = tail.X - 1, Y = tail.Y + 1 };
            goto end;
        }
        
        if (tail.Y > head.Y && tail.X > head.X)
        {
            tail = tail with { X = tail.X - 1, Y = tail.Y - 1 };
            goto end;
        }
        
        tail = tail with { X = tail.X + 1, Y = tail.Y - 1 };
        
        end:
        points[1] = tail;

        return MoveTailKnots(points[1..]);
    }
}