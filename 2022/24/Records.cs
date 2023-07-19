namespace _24;

public readonly record struct Point(int X, int Y)
{
    public IReadOnlySet<Point> AdjacentPoints => new HashSet<Point>
        { this with { X = X + 1 }, this with { X = X - 1 }, this with { Y = Y + 1 }, this with { Y = Y - 1 } };
}