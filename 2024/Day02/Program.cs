Console.WriteLine(File.ReadAllLines("Input.txt")
    .Select(x => x.Split(' '))
    .Select(x => x.Select(int.Parse).ToList())
    .Select(ToDiffList)
    .Select(x => x.ToList())
    .Count(x => x.All(y => y is 1 or 2 or 3) || x.All(y => y is -1 or -2 or -3)));

IEnumerable<int> ToDiffList(IList<int> arr)
{
    for (var i = 1; i < arr.Count; i++) yield return arr[i] - arr[i - 1];
}