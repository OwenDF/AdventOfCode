namespace _21;

public static class Functions
{
    public static void CreateMonkey(Dictionary<string, Func<long>> monkeys, string input)
    {
        var split = input.Split(' ');
        var name = split[0].Substring(0, 4);
        Func<long> func;

        func = int.TryParse(split[1], out var number)
            ? () => number
            : split[2][0] switch
            {
                '*' => () => monkeys[split[1]]() * monkeys[split[3]](),
                '/' => () => monkeys[split[1]]() / monkeys[split[3]](),
                '+' => () => monkeys[split[1]]() + monkeys[split[3]](),
                '-' => () => monkeys[split[1]]() - monkeys[split[3]]()
            };

        monkeys.Add(name, func);
    }
}