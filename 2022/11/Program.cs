﻿using _11;

var monkeys = new List<Monkey>
{
    new(new Queue<long>(new long[] {99, 63, 76, 93, 54, 73}), x => x * 11, x => x % 2 is 0, 7, 1),
    new(new Queue<long>(new long[] {91, 60, 97, 54}), x => x + 1, x => x % 17 is 0, 3, 2),
    new(new Queue<long>(new long[] {65}), x => x + 7, x => x % 7 is 0, 6, 5),
    new(new Queue<long>(new long[] {84, 55}), x => x + 3, x => x % 11 is 0, 2, 6),
    new(new Queue<long>(new long[] {86, 63, 79, 54, 83}), x => x * x, x => x % 19 is 0, 7, 0),
    new(new Queue<long>(new long[] {96, 67, 56, 95, 64, 69, 96}), x => x + 4, x => x % 5 is 0, 4, 0),
    new(new Queue<long>(new long[] {66, 94, 70, 93, 72, 67, 88, 51}), x => x * 5, x => x % 13 is 0, 4, 5),
    new(new Queue<long>(new long[] {59, 59, 74}), x => x + 8, x => x % 3 is 0, 1, 3)
};

var magicModulo = 2 * 17 * 7 * 11 * 19 * 5 * 13 * 3;

for (var i = 0; i < 10_000; i++)
{
    foreach (var monkey in monkeys)
    {
        while (monkey.Items.TryDequeue(out var item))
        {
            monkey.InspectionCounter++;
            var newWorry = monkey.Operation(item);
            var destination = monkey.Test(newWorry)
                ? monkey.TrueDestination
                : monkey.FalseDestination;
            monkeys[destination].Items.Enqueue(newWorry % magicModulo);
        }
    }
}

Console.WriteLine(monkeys.Select(x => x.InspectionCounter).OrderDescending().Take(2).Aggregate((c, n) => c * n)); 