namespace _4;

public static class Functions
{
    public static bool FullyOverlaps(ElfPair pair) =>
        (pair.First.Lower <= pair.Second.Lower && pair.First.Upper >= pair.Second.Upper) ||
        (pair.First.Lower >= pair.Second.Lower && pair.First.Upper <= pair.Second.Upper);

    public static ElfPair ToElfPair(string input)
    {
        var split = input.Split(',');
        var first = split[0].ToAssignment();
        var second = split[1].ToAssignment();
        return new(first, second);
    }

    public static Assignment ToAssignment(this string input)
    {
        var split = input.Split('-');
        return new(int.Parse(split[0]), int.Parse(split[1]));
    }
}