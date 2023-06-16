namespace _17;

public class Cross : Rock 
{
    public Cross(HashSet<Point> caveContext, int height) : base(caveContext, height + 1) { }
    
    public override void MoveLeft()
    {
        if (CaveContext.Contains(Position with { X = Position.X - 1 }) ||
            CaveContext.Contains(Position with { Y = Position.Y - 1 }) ||
            CaveContext.Contains(Position with { Y = Position.Y + 1 }) ||
            Position.X is 0) return;

        Position = Position with { X = Position.X - 1 };
    }

    public override void MoveRight()
    {
        if (CaveContext.Contains(Position with { X = Position.X + 3 }) ||
            CaveContext.Contains(new(Position.X + 2, Position.Y - 1)) ||
            CaveContext.Contains(new(Position.X + 2, Position.Y + 1)) ||
            Position.X is 4) return;

        Position = Position with { X = Position.X + 1 };
    }

    public override int? Drop()
    {
        if (CaveContext.Contains(Position with { Y = Position.Y - 1 }) ||
            CaveContext.Contains(new(Position.X + 1, Position.Y - 2)) ||
            CaveContext.Contains(new(Position.X + 2, Position.Y - 1))) 
        {
            CaveContext.AddNew(Position);
            CaveContext.AddNew(Position with { X = Position.X + 1 });
            CaveContext.AddNew(Position with { X = Position.X + 2 });
            CaveContext.AddNew(new(Position.X + 1, Position.Y + 1));
            CaveContext.AddNew(new(Position.X + 1, Position.Y - 1));

            return Position.Y + 1;
        }
        
        Position = Position with { Y = Position.Y - 1 };
        return null;
    }

    public override Point[] GetFullCurrentPosition()
    {
        var CaveContext = new HashSet<Point>();
        CaveContext.AddNew(Position);
        CaveContext.AddNew(Position with { X = Position.X + 1 });
        CaveContext.AddNew(Position with { X = Position.X + 2 });
        CaveContext.AddNew(new(Position.X + 1, Position.Y + 1));
        CaveContext.AddNew(new(Position.X + 1, Position.Y - 1));

        return CaveContext.ToArray();
    }
}