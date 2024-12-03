Console.WriteLine(File.ReadAllLines("Input.txt")
    .Select(x => x.Split(' '))
    .Select(x => x.Select(int.Parse).ToList())
    .Select(ToDiffLists)
    .Select(x => x.ToList())
    .Count(x => x.Any(y => y.All(z => z is 1 or 2 or 3)) || x.Any(y => y.All(z => z is -1 or -2 or -3))));

IEnumerable<IEnumerable<int>> ToDiffLists(IList<int> arr)
{
    yield return ToDiffList(arr);
    for (var i = 0; i < arr.Count; i++)
    {
        var copyList = new List<int>(arr);
        copyList.RemoveAt(i);
        yield return ToDiffList(copyList);
    }
}

IEnumerable<int> ToDiffList(IList<int> arr)
{
    for (var i = 1; i < arr.Count; i++) yield return arr[i] - arr[i - 1];
}