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

    public static int[] GetAdjacentNumbers(Point p, Dictionary<Point, Box<int>> numbers) =>
        new[]
        {
            numbers.TryGetValue(new Point(p.X - 1, p.Y + 1), out var a) ? a : null,
            numbers.TryGetValue(new Point(p.X + 1, p.Y - 1), out var b) ? b : null,
            numbers.TryGetValue(new Point(p.X + 1, p.Y + 1), out var c) ? c : null,
            numbers.TryGetValue(new Point(p.X - 1, p.Y - 1), out var d) ? d : null,
            numbers.TryGetValue(p with { X = p.X - 1 }, out var e) ? e : null,
            numbers.TryGetValue(p with { X = p.X + 1 }, out var f) ? f : null,
            numbers.TryGetValue(p with { Y = p.Y - 1 }, out var g) ? g : null,
            numbers.TryGetValue(p with { Y = p.Y + 1 }, out var h) ? h : null,
        }.Where(x => x is not null).GroupBy(x => x).Select(x => x.Key!.Value).ToArray();

    public static int ToNum(this char c) => int.Parse(c.ToString());
    
    public static bool IsPartSymbol(this char c) => !(c is '.' || char.IsDigit(c));
}

public record struct Point(int X, int Y);

public class Box<T>(T value)
{
    public T Value { get; set; } = value;
}