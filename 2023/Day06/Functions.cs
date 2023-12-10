namespace Day06;

internal static class Functions
{
    public static int CalculateRaceCombinationTotal(Race race)
    {
        var total = 0;
        for (var i = 1; i < race.TimeLimit; i++)
        {
            if (i * (race.TimeLimit - i) > race.BestDistance) total++;
        }

        return total;
    }
}

internal record Race(int TimeLimit, int BestDistance);