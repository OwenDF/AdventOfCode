namespace _24;

public record BlizzardState(IReadOnlySet<Point> NorthBlizzards, IReadOnlySet<Point> EastBlizzards,
    IReadOnlySet<Point> SouthBlizzards, IReadOnlySet<Point> WestBlizzards)
{
    public bool Contains(Point p) => NorthBlizzards.Contains(p) || EastBlizzards.Contains(p) ||
                                     SouthBlizzards.Contains(p) || WestBlizzards.Contains(p);

    public BlizzardState Progress(int maxX, int maxY) =>
        new(NorthBlizzards.Select(b => b with { Y = b.Y is 1 ? maxY : b.Y - 1 }).ToHashSet(),
            EastBlizzards.Select(b => b with { X = b.X == maxX ? 1 : b.X + 1 }).ToHashSet(),
            SouthBlizzards.Select(b => b with { Y = b.Y == maxY ? 1 : b.Y + 1 }).ToHashSet(),
            WestBlizzards.Select(b => b with { X = b.X is 1 ? maxX : b.X - 1 }).ToHashSet());

}