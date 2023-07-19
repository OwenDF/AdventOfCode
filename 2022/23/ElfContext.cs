namespace _23;

public record ElfContext(Point C, Point N, Point NE, Point E, Point SE, Point S, Point SW, Point W, Point NW)
{
    public bool CanMoveNorth(IReadOnlySet<Point> otherElves) =>
        !otherElves.Contains(NW) && !otherElves.Contains(N) && !otherElves.Contains(NE);

    public bool CanMoveSouth(IReadOnlySet<Point> otherElves) =>
        !otherElves.Contains(SE) && !otherElves.Contains(S) && !otherElves.Contains(SW);

    public bool CanMoveEast(IReadOnlySet<Point> otherElves) =>
        !otherElves.Contains(NE) && !otherElves.Contains(E) && !otherElves.Contains(SE);

    public bool CanMoveWest(IReadOnlySet<Point> otherElves) =>
        !otherElves.Contains(SW) && !otherElves.Contains(W) && !otherElves.Contains(NW);

    public bool IsIsolated(IReadOnlySet<Point> otherElves) =>
        CanMoveNorth(otherElves) && CanMoveEast(otherElves) && CanMoveSouth(otherElves) && CanMoveWest(otherElves);
}