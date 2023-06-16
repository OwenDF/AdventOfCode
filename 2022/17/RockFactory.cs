namespace _17;

public class RockFactory
{
    private readonly Func<HashSet<Point>, int, Rock>[] _rockCreators;
    
    public RockFactory()
    {
        _rockCreators = new Func<HashSet<Point>, int, Rock>[]
        {
            (c, p) => new Horizontal(c, p),
            (c, p) => new Cross(c, p),
            (c, p) => new ReverseL(c, p),
            (c, p) => new Vertical(c, p),
            (c, p) => new Square(c, p)
        };
    }

    public Rock GetNextRock(HashSet<Point> context, int startHeight, int rockNumber) =>
        _rockCreators[rockNumber % _rockCreators.Length](context, startHeight);

}