namespace _22;

public readonly record struct Point(int X, int Y);
public record Map(IReadOnlySet<Point> OpenSpaces, IReadOnlySet<Point> Walls, int MaxX, int MaxY);
public record Instruction;
public record TurnInstruction(char Direction) : Instruction;
public record MoveInstruction(int Count) : Instruction;
public record Position(char Direction, Point Location);