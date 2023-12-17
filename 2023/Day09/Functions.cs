namespace Day09;

internal static class Functions
{
    public static int ExtrapolateNextValue(int[] values) 
    {
        if (values.All(x => x is 0)) return 0;

        var differences = new int[values.Length - 1];
        for (var i = 0; i < differences.Length; i++) differences[i] = values[i + 1] - values[i];

        return values.Last() + ExtrapolateNextValue(differences);
    }
}