using static _13.Functions;

namespace _13;

public class PacketComparer : IComparer<Element>
{
    public int Compare(Element? x, Element? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));

        return AreElementsOrdered(x, y) switch
        {
            false => 1,
            true => -1,
            _ => 0
        };
    }
}