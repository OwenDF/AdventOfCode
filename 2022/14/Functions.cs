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

    public static int GetTotalSandfall(HashSet<Point> blockers)
    {
        var sandStart = new Point(500, 0);
        var yMax = blockers.MaxBy(x => x.Y).Y + 1;
        
        for (var i = 0;;i++)
        {
            if (blockers.Contains(sandStart))
            {
                return i;
            }

            AddSandParticle(blockers, sandStart, yMax);
        }
    }

    private static void AddSandParticle(HashSet<Point> blockers, Point sandStart, int yMax)
    {

        var sandParticle = sandStart;
        while (true)
        {
            if (sandParticle.Y == yMax)
            {
                blockers.Add(sandParticle);
                break;
            }

            var down = sandParticle with { Y = sandParticle.Y + 1 };
            var downLeft = new Point(sandParticle.X - 1, sandParticle.Y + 1);
            var downRight = new Point(sandParticle.X + 1, sandParticle.Y + 1);

            if (!blockers.Contains(down))
            {
                sandParticle = down;
                continue;
            }

            if (!blockers.Contains(downLeft))
            {
                sandParticle = downLeft;
                continue;
            }
        
            if (!blockers.Contains(downRight))
            {
                sandParticle = downRight;
                continue;
            }
        
            blockers.Add(sandParticle);
            break;
        }
    }
}