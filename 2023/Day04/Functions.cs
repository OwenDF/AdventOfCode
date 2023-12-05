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

    public static int CalculateScore(ScratchCard card) => card.CardNumbers.Intersect(card.WinningNumbers).Count();
}

internal record ScratchCard(IReadOnlySet<int> CardNumbers, IReadOnlySet<int> WinningNumbers);