namespace _9;

public static class Functions
{
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
}