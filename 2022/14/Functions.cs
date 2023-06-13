namespace _14;

public static class Functions
{
    public static Point ToPoint(this string input)
    {
        var split = input.Split(',');
        return new Point(int.Parse(split[0]), int.Parse(split[1]));
    }

    public static void AddBlockers(HashSet<Point> blockers, Point start, Point finish)
    {
        if (start.X < finish.X)
        {
            while (start != finish)
            {
                start = start with { X = start.X + 1 };
                blockers.Add(start);
            }
        }

        if (start.X > finish.X)
        {
            while (start != finish)
            {
                start = start with { X = start.X - 1 };
                blockers.Add(start);
            }
        }
        
        if (start.Y < finish.Y)
        {
            while (start != finish)
            {
                start = start with { Y = start.Y + 1 };
                blockers.Add(start);
            }
        }
        
        if (start.Y > finish.Y)
        {
            while (start != finish)
            {
                start = start with { Y = start.Y - 1 };
                blockers.Add(start);
            }
        }
    }
}