using Day05;
using static Day05.Functions;

var sections = File.ReadAllText("Input.txt").Split("\n\n")
    .Select(x => x.Split('\n', StringSplitOptions.RemoveEmptyEntries)).ToList();

var seedRanges = sections[0][0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse)
    .CreateRanges().ToList();

var maps = sections.Skip(1).Select(ToMapList).ToDictionary(x => x.FromType, x => x);

Console.WriteLine(seedRanges.ConvertToDestinationRanges(maps, "seed", "location").MinBy(x => x.InclusiveStart)
    .InclusiveStart);
