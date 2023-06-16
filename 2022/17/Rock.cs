namespace _17;

public abstract class Rock 
{
    protected readonly HashSet<Point> CaveContext;
    public Point Position { get; protected set; }

    protected Rock(HashSet<Point> caveContext, int height)
    {
        CaveContext = caveContext;
        Position = new(2, height);
    }

    public abstract void MoveLeft();
    public abstract void MoveRight();
    public abstract int? Drop();
    public abstract Point[] GetFullCurrentPosition();
}