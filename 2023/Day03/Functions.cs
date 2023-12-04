namespace Day03;

internal static class Functions
{
    public static bool IsPartNumber(this Point p, char[,] grid, int maxX, int maxY)
    {
        if (p is { X: > 0, Y: > 0 } && grid[p.X - 1, p.Y - 1].IsPartSymbol()) return true;
        if (p.X > 0 && grid[p.X - 1, p.Y].IsPartSymbol()) return true;
        if (p.X > 0 && p.Y < maxY && grid[p.X - 1, p.Y + 1].IsPartSymbol()) return true;
        if (p.Y > 0 && grid[p.X, p.Y - 1].IsPartSymbol()) return true;
        if (p.Y < maxY && grid[p.X, p.Y + 1].IsPartSymbol()) return true;
        if (p.X < maxX && grid[p.X + 1, p.Y].IsPartSymbol()) return true;
        if (p.X < maxX && p.Y > 0 && grid[p.X + 1, p.Y - 1].IsPartSymbol()) return true;
        if (p.X < maxX && p.Y < maxY && grid[p.X + 1, p.Y + 1].IsPartSymbol()) return true;

        return false;
    }
    
    public static bool IsPartSymbol(this char c) => !(c is '.' || char.IsDigit(c));
}

public record struct Point(int X, int Y);