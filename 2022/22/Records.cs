namespace _22;

public readonly record struct Point(int X, int Y);
public record Instruction;
public record TurnInstruction(char Direction) : Instruction;
public record MoveInstruction(int Count) : Instruction;
public enum FaceId { Unknown, Bottom, Top, North, South, West, East }
public record Position(char Direction, Point Location, FaceId Face);
public record Cube(Face Bottom, Face Top, Face North, Face South, Face West, Face East);
public record Face(IReadOnlySet<Point> Walls);