namespace _25;

public static class Functions
{
    public static long ToLong(this Snafu snafu)
    {
        var num = 0L;
        var snafuString = snafu.Raw.Reverse().ToList();

        for (var i = 0; i < snafuString.Count; i++)
        {
            num += snafuString[i].ToDecimalDigit() * 5.Pow(i);
        }

        return num;
    }

    public static Snafu ToSnafu(this long num)
    {
        var snafuString = "";
        var bases = RetrieveBases(num);

        foreach (var bas in bases) 
        {
            if (num >= bas.Min + bas.Multiplier)
            {
                num -= bas.Multiplier * 2;
                snafuString += "2";
                continue;
            }
            
            if (num >= bas.Min)
            {
                num -= bas.Multiplier;
                snafuString += "1";
                continue;
            }
            
            if (num <= - bas.Min - bas.Multiplier)
            {
                num += bas.Multiplier * 2;
                snafuString += "=";
                continue;
            }
            
            if (num <= - bas.Min)
            {
                num += bas.Multiplier * 1;
                snafuString += "-";
                continue;
            }

            snafuString += "0";
        }

        if (snafuString[0] == '0') snafuString = snafuString[1..];
        return new(snafuString);
    }

    private static IReadOnlyList<Base> RetrieveBases(long num)
    {
        var list = new List<Base> { new(1, 1), new(3, 5), new(13, 25) };

        while (list.Last().Min < num)
        {
            var multiplier = list.Last().Multiplier * 5;
            list.Add(new(multiplier + list.Select(x => x.Multiplier * -2).Sum() , multiplier));
        }

        list.Reverse();
        return list;
    }

    private static int ToDecimalDigit(this char snafuDigit) => snafuDigit switch
    {
        '2' => 2,
        '1' => 1,
        '0' => 0,
        '-' => -1,
        '=' => -2
    };

    private static long Pow(this int bas, int exp)
    {
        var num = 1L;

        while (exp > 0)
        {
            num *= bas;
            exp--;
        }

        return num;
    }
}