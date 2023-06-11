using _10;
using static _10.Functions;

var instructions = (await File.ReadAllLinesAsync("Input.txt")).Select(ToInstruction).ToArray();

var xRegValues = new List<int> { 0, 1 };
var current = 1;
var sum = 0;

foreach (var instruction in instructions)
{
    if (instruction is NoOp)
    {
        xRegValues.Add(current);
        continue;
    }
    
    if (instruction is AddInstruction ai)
    {
        xRegValues.Add(current);
        current += ai.Amount;
        xRegValues.Add(current);
    }
}

for (var i = 20; i < xRegValues.Count; i += 40)
{
    sum += xRegValues[i] * i;
}

var screen = new List<char>();

for (var i = 0; i < xRegValues.Count - 1; i++)
{
    if (xRegValues[i + 1] - (i % 40) is 0 or 1 or -1)
    {
        screen.Add('#');
        continue;
    }

    screen.Add(' ');
}

Console.WriteLine(sum);
Console.WriteLine();
screen.Chunk(40).Select(x => new string(x)).ToList().ForEach(Console.WriteLine);