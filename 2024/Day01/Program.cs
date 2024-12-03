var lines = File.ReadAllLines("Input.txt");

var (left, right) = (new List<int>(), new List<int>());
foreach (var line in lines)
{
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    left.Add(int.Parse(parts[0]));
    right.Add(int.Parse(parts[1]));
}

left.Sort();
right.Sort();

var rightCount = right.GroupBy(x => x).ToDictionary(x => x.Key, x => x.Count());

var total = left.Sum(number => number * rightCount.GetValueOrDefault(number, 0));

Console.WriteLine(total);