namespace _11;

public class Monkey
{
    public Monkey(
        Queue<int> items,
        Func<int, int> operation,
        Func<int, bool> test,
        int trueDestination,
        int falseDestination)
    {
        Items = items;
        Operation = operation;
        Test = test;
        TrueDestination = trueDestination;
        FalseDestination = falseDestination;
    }
    public Queue<int> Items { get; }
    public Func<int, int> Operation { get; }
    public Func<int, bool> Test { get; }
    public int TrueDestination { get; }
    public int FalseDestination { get; }
    public int InspectionCounter { get; set; }
}