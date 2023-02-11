namespace _2;

public enum Move { Rock = 1, Paper = 2, Scissors = 3 }
public record struct Round(Move Theirs, Move Mine);