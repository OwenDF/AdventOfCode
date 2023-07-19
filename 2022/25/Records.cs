namespace _25;

public readonly record struct Snafu(string Raw);

public readonly record struct Base(long Min, long Multiplier);