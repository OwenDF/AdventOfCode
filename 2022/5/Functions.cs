namespace _5;

public static class Functions
{
    public static Dictionary<int, Stack<Crate>> CreateWarehouse(string[] input)
    {
        var indexDictionary = new Dictionary<int, int>();
        var warehouse = new Dictionary<int, Stack<Crate>>();
        for (var i = 0; i < input[0].Length; i++)
        {
            if (input[0][i] != ' ')
            {
                var stackNumber = int.Parse(input[0][i].ToString());
                indexDictionary.Add(i, stackNumber);
                warehouse.Add(stackNumber, new Stack<Crate>());
            }
        }
        
        foreach (var row in input.Skip(1))
        {
            foreach (var index in indexDictionary.Keys)
            {
                if (row[index] != ' ') warehouse[indexDictionary[index]].Push(new(row[index]));
            }
        }

        return warehouse;
    }

    public static CraneInstruction CreateInstruction(string input)
    {
        var split = input.Split(' ');
        return new CraneInstruction(int.Parse(split[3]), int.Parse(split[5]), int.Parse(split[1]));
    }

    public static Dictionary<int, Stack<Crate>> Perform(
        this CraneInstruction instruction,
        Dictionary<int, Stack<Crate>> warehouse)
    {
        var source = warehouse[instruction.StackFrom];
        var destination = warehouse[instruction.StackTo];
        var miniStack = new Stack<Crate>();

        for (var i = 0; i < instruction.CrateCount; i++)
        {
            var crate = source.Pop();
            miniStack.Push(crate);
        }
        
        while (miniStack.Count > 0)
        {
            var crate = miniStack.Pop();
            destination.Push(crate);
        }

        return warehouse;
    }
}