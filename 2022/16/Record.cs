namespace _16;

public sealed record Valve(string Name, int FlowRate, IReadOnlyList<string> AdjacentValves)
{
    public bool Equals(Valve? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return Name == other.Name && FlowRate == other.FlowRate && AdjacentValves.SequenceEqual(other.AdjacentValves);
    }

    public override int GetHashCode()
    {
        var collectionHashCode = AdjacentValves.Aggregate(0, HashCode.Combine);
        return HashCode.Combine(Name, FlowRate, collectionHashCode);
    }
}

public sealed record ValvePair(Valve First, Valve Second)
{
    public bool Equals(ValvePair? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return new[] { First, Second }
            .OrderBy(x => x.Name).SequenceEqual(new[] { other.First, other.Second }.OrderBy(x => x.Name));
    }

    public override int GetHashCode() => new[] { First, Second }.OrderBy(x => x.Name).Aggregate(0, HashCode.Combine);
}

public record ValvePairDistance(ValvePair Pair, int Distance);
public record Move(Valve Destination, int Distance);
public record PressureRelease(string ValveName, int CumulativeRelease, int Time);