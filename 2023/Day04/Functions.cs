namespace Day04;

internal static class Functions
{
    public static ScratchCard ToScratchCard(string data)
    {
        var sections = data.Split(':')[1].Split('|');

        return new ScratchCard(
            sections[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet(),
            sections[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet());
    }

    public static int CalculateScore(ScratchCard card)
    {
        var union = new HashSet<int>(card.CardNumbers);
        union.IntersectWith(card.WinningNumbers);
        if (union.Count is 0) return 0;

        return union.Skip(1).Aggregate(1, (c, _) => c * 2);
    }
}

internal record ScratchCard(IReadOnlySet<int> CardNumbers, IReadOnlySet<int> WinningNumbers);