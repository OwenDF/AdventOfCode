namespace Day05;

internal static class Functions
{
    public static MapList ToMapList(string[] section)
    {
        var names = section[0].Split(' ')[0].Split('-');
        return new MapList(names[0], names[2], section.Skip(1).Select(ToMap)
            // Filthy hack
            .Append(new(new(long.MaxValue - 1, long.MaxValue), 0))
            .OrderBy(x => x.SourceRange.InclusiveStart).ToArray());
    }

    public static IEnumerable<Range> CreateRanges(this IEnumerable<long> nums)
    {
        using var enumerator = nums.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var first = enumerator.Current;
            enumerator.MoveNext();
            var second = enumerator.Current;
            yield return new Range(first, first + second);
        }
    }

    public static IReadOnlyList<Range> ConvertToDestinationRanges(
        this IReadOnlyList<Range> ranges,
        Dictionary<string, MapList> mapLists,
        string mapName,
        string lastMapName)
    {
        if (mapName == lastMapName) return ranges;

        var mapList = mapLists[mapName];
        var convertedRanges = ranges.Select(r => GetDestinationRanges(mapList.Maps, r)).SelectMany(x => x).ToList();

        return ConvertToDestinationRanges(convertedRanges, mapLists, mapList.ToType, lastMapName);
    }

    private static IEnumerable<Range> GetDestinationRanges(Span<Map> maps, Range sourceRange)
    {
        if (sourceRange.ExclusiveEnd <= sourceRange.InclusiveStart) return Enumerable.Empty<Range>();

        var map = maps[0];

        if (sourceRange.InclusiveStart < map.SourceRange.InclusiveStart)
        {
            return GetDestinationRanges(maps, sourceRange with { InclusiveStart = map.SourceRange.InclusiveStart })
                .Append(sourceRange with
                {
                    ExclusiveEnd = Math.Min(map.SourceRange.InclusiveStart, sourceRange.ExclusiveEnd)
                });
        }

        if (sourceRange.InclusiveStart >= map.SourceRange.ExclusiveEnd)
            return GetDestinationRanges(maps[1..], sourceRange);

        if (sourceRange.InclusiveStart >= map.SourceRange.InclusiveStart)
        {
            return GetDestinationRanges(maps[1..], sourceRange with { InclusiveStart = map.SourceRange.ExclusiveEnd })
                .Append((sourceRange with
                {
                    ExclusiveEnd = Math.Min(sourceRange.ExclusiveEnd, map.SourceRange.ExclusiveEnd)
                }).Offset(map.DestinationOffset));
        }

        throw new Exception("unreachable");
    }
    
    private static Range Offset(this Range src, long offSet) 
        => new(src.InclusiveStart + offSet, src.ExclusiveEnd + offSet);

    private static bool InRange(this long num, Range range) => num >= range.InclusiveStart && num < range.ExclusiveEnd;
    
    private static Map ToMap(string data)
    {
        var nums = data.Split(' ');
        return new(
            new(long.Parse(nums[1]), long.Parse(nums[1]) + long.Parse(nums[2])),
            long.Parse(nums[0]) - long.Parse(nums[1]));
    }
}

internal record struct Range(long InclusiveStart, long ExclusiveEnd);
internal record Map(Range SourceRange, long DestinationOffset);
internal record MapList(string FromType, string ToType, Map[] Maps);