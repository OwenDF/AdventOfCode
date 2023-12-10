namespace Day07;

internal static class Functions
{
    public static CardHand ToCardHand(string handString)
    {
        var both = handString.Split(' ');
        return new CardHand(new Cards(both[0].Select(x => new Card(x))), int.Parse(both[1]));
    }
}

internal class Cards : IComparable<Cards>
{
    private const int FixedSize = 5;
    private static readonly char[] CardRank = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };
    private readonly Card[] _items;

    public Cards(IEnumerable<Card> items)
    {
        _items = items.ToArray();
        if (_items.Length is not FixedSize) throw new Exception();
    }

    public int CompareTo(Cards? other)
    {
        ArgumentNullException.ThrowIfNull(other);
        if (Equals(other)) return 0;
        
        var thisInstanceHandValue = GetHandTypeValue();
        var otherInstanceHandValue = other.GetHandTypeValue();

        if (thisInstanceHandValue > otherInstanceHandValue) return 1;
        if (thisInstanceHandValue < otherInstanceHandValue) return -1;

        return CompareIndividualCards(_items, other._items);
    }

    public override bool Equals(object? other) => other is Cards && Equals(other);

    public override int GetHashCode() => _items.Aggregate(0, (c, n) => HashCode.Combine(c, n.GetHashCode()));
    

    private bool Equals(Cards other) => _items.SequenceEqual(other._items);

    private int GetHandTypeValue()
    {
        var grouped = _items.GroupBy(x => x).ToList();

        return grouped.Count switch
        {
            1 => 7,
            2 when grouped.Any(x => x.Count() is 4) => 6,
            2 => 5,
            3 when grouped.Any(x => x.Count() is 3) => 4,
            3 => 3,
            4 => 2,
            5 => 1,
            _ => throw new Exception()
        };
    }

    private int CompareIndividualCards(Span<Card> first, Span<Card> second)
    {
        if (first.Length != second.Length) throw new Exception();
        if (first.Length is 0) return 0;

        var firstScore = Array.IndexOf(CardRank, first[0].CardType);
        var secondScore = Array.IndexOf(CardRank, second[0].CardType);

        if (firstScore > secondScore) return 1;
        if (firstScore < secondScore) return -1;

        return CompareIndividualCards(first[1..], second[1..]);
    }
}

internal record Card(char CardType);
internal record CardHand(Cards Cards, int Bid);