var bla = File.ReadAllLines("Input.txt")
    .Select(ToEquation)
    .Where(x => EquationCanBeSolved(x.Nums.Span[0], x.Nums.Span[1..], x.Target));

Console.WriteLine(File.ReadAllLines("Input.txt")
    .Select(ToEquation)
    .Where(x => EquationCanBeSolved(x.Nums.Span[0], x.Nums.Span[1..], x.Target))
    .Sum(x => x.Target));

bool EquationCanBeSolved(long runningTotal, ReadOnlySpan<int> remainingNums, long target)
{
    if (runningTotal > target) return false;
    if (remainingNums.IsEmpty) return runningTotal == target;
    
    return EquationCanBeSolved(runningTotal + remainingNums[0], remainingNums[1..], target) ||
           EquationCanBeSolved(runningTotal * remainingNums[0], remainingNums[1..], target) ||
           EquationCanBeSolved(long.Parse(runningTotal.ToString() + remainingNums[0]), remainingNums[1..], target);
}

Equation ToEquation(string inputLine)
{
    var split = inputLine.Split(':', StringSplitOptions.RemoveEmptyEntries);
    var target = long.Parse(split[0]);

    var nums = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    return new(target, nums);
}

record Equation(long Target, ReadOnlyMemory<int> Nums);