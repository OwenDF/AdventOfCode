namespace _2;

public enum Move { Rock = 1, Paper = 2, Scissors = 3 }
public enum Result { Lose = 0 , Draw = 3, Win = 6 }
public record struct Round(Move Theirs, Result Result);