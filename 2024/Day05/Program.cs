var sections = File.ReadAllText("Input.txt").Split("\n\n", StringSplitOptions.RemoveEmptyEntries);

var middleSum = 0;
var rules = CreateRules(sections[0]);

var printingLists = sections[1].Split('\n', StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split(',', StringSplitOptions.RemoveEmptyEntries))
    .Select(x => x.Select(int.Parse).ToList()).ToList();

var ruleRunner = CreateRuleRunner(rules);

foreach (var list in printingLists)
{
    var items = new HashSet<int>();
    foreach (var number in list)
    {
        items.Add(number);
        if (rules[number].Intersect(items).Any()) goto fixy;
    }

    continue;

    fixy: ;
    list.Sort(ruleRunner);
    middleSum += list[list.Count / 2];
}

Console.WriteLine(middleSum);

Comparison<int> CreateRuleRunner(Dictionary<int, ISet<int>> rules)
{
    return (x, y) =>
    {
        if (rules.TryGetValue(x, out var mustBeAfterX) && mustBeAfterX.Contains(y)) return -1;
        if (rules.TryGetValue(y, out var mustBeAfterY) && mustBeAfterY.Contains(x)) return 1;
        return 0;
    };
}

Dictionary<int, ISet<int>> CreateRules(string rules)
{
    var ruleItems = rules.Split('\n', StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Split('|'))
        .Select(x => (int.Parse(x[0]), int.Parse(x[1])));

    var result = new Dictionary<int, ISet<int>>();
    
    foreach (var (left, right) in ruleItems)
    {
        var list = result.TryGetValue(left, out var l) ? l : new HashSet<int>();
        list.Add(right);
        result[left] = list;
    }

    return result;
}