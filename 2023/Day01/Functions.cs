namespace Day01;

internal static class Functions
{
    private static readonly string[] DigitStrings =
        { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

    
    public static IReadOnlyList<int> ToDigitLine(string line)
    {
        var list = new List<int>();
        for (var i = 0; i < line.Length; i++)
        {
            var character = line[i];
            if (char.IsAsciiDigit(character))
            {
                list.Add(int.Parse(character.ToString()));
                continue;
            }

            for (var j = 0; j < 10; j++)
            {
                if (i + DigitStrings[j].Length > line.Length) continue;
                var substr = line.Substring(i, DigitStrings[j].Length);
                if (substr == DigitStrings[j]) list.Add(j);
            }
        }

        return list;
    }
}