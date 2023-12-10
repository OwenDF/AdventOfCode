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

    private const int FiveOfAKind = 7;
    private const int FourOfAKind = 6;
    private const int FullHouse = 5;
    private const int ThreeOfAKind = 4;
    private const int TwoPair = 3;
    private const int Pair = 2;
    private const int HighCard = 1;
    
    private static readonly char[] CardRank = { 'J','2', '3', '4', '5', '6', '7', '8', '9', 'T', 'Q', 'K', 'A' };
    private static readonly Card Joker = new('J');
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
        var jackCount = _items.Count(x => x == Joker);

        return grouped.Count switch
        {
            1 => FiveOfAKind,
            2 when jackCount is not 0 => FiveOfAKind,
            2 when grouped.Any(x => x.Count() is 4) => FourOfAKind,
            2 => FullHouse,
            3 when jackCount is 3 => FourOfAKind,
            3 when grouped.Any(x => x.Count() is 3) && jackCount is 1 => FourOfAKind,
            3 when jackCount is 2 => FourOfAKind,
            3 when jackCount is 1 => FullHouse,
            3 when grouped.Any(x => x.Count() is 3) => ThreeOfAKind,
            3 => TwoPair,
            4 when jackCount is not 0 => ThreeOfAKind,
            4 => Pair,
            5 when jackCount is 1 => Pair,
            5 => HighCard,
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