namespace _4;

public record ElfPair(Assignment First, Assignment Second);
public record Assignment(int Lower, int Upper);