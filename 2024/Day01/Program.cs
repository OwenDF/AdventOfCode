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

var total = 0;
for (var i = 0; i < left.Count; i++) total += Math.Abs(left[i] - right[i]);

Console.WriteLine(total);