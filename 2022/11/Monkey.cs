namespace _11;

public class Monkey
{
    public Monkey(
        Queue<long> items,
        Func<long, long> operation,
        Func<long, bool> test,
        int trueDestination,
        int falseDestination)
    {
        Items = items;
        Operation = operation;
        Test = test;
        TrueDestination = trueDestination;
        FalseDestination = falseDestination;
    }
    public Queue<long> Items { get; }
    public Func<long, long> Operation { get; }
    public Func<long, bool> Test { get; }
    public int TrueDestination { get; }
    public int FalseDestination { get; }
    public long InspectionCounter { get; set; }
}