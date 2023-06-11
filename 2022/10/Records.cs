namespace _10;

public record Instruction;
public record AddInstruction(int Amount) : Instruction;
public record NoOp : Instruction;