namespace _13;

public record UnparsedPair(string First, string Second);
public record Element;
public record DigitElement(int Value) : Element;
public sealed record ArrayElement(IReadOnlyList<Element> Values) : Element 
{
    public bool Equals(ArrayElement? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Values.Count == other.Values.Count && Values.SequenceEqual(other.Values);
    }

    public override int GetHashCode() => Values.Aggregate(base.GetHashCode(), HashCode.Combine);
}