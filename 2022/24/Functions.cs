namespace _24;

public static class Functions
{
    public static BlizzardState ReadInitialBlizzardState(string[] input)
    {
        var northBlizz = new HashSet<Point>();
        var southBlizz = new HashSet<Point>();
        var eastBlizz = new HashSet<Point>();
        var westBlizz = new HashSet<Point>();
        
        for (var j = 0; j < input.Length; j++)
        for (var i = 0; i < input[j].Length; i++)
        {
            var p = new Point(i, j);
            switch (input[j][i])
            {
                case '^':
                    northBlizz.Add(p);
                    break;
                case 'v':
                    southBlizz.Add(p);
                    break;
                case '>':
                    eastBlizz.Add(p);
                    break;
                case '<':
                    westBlizz.Add(p);
                    break;
            }
        }

        return new(northBlizz, eastBlizz, southBlizz, westBlizz);
    }

    public static bool IsSafePoint(this Point point, BlizzardState blizzardState, Point entrance, Point exit, int maxX,
        int maxY)
    {
        if ((point.Y < 1 && point != entrance) || point.X < 1 || (point.Y > maxY && point != exit) || point.X > maxX)
            return false;

        if (blizzardState.Contains(point)) return false;

        return true;
    }
}