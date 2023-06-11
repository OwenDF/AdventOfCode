namespace _10;

public static class Functions
{
    public static Instruction ToInstruction(string instruction) => 
        instruction is "noop" ? new NoOp() : new AddInstruction(int.Parse(instruction.Split(' ')[1]));
}