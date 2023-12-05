namespace Day05;

internal static class Functions
{
    public static MapList ToMapList(string[] section)
    {
        var names = section[0].Split(' ')[0].Split('-');
        return new MapList(names[0], names[2], section.Skip(1).Select(ToMap).ToList());
    }

    public static long ConvertToValue(
        this long val,
        Dictionary<string, MapList> mapLists,
        string mapName,
        string lastMapName)
    {
        if (mapName == lastMapName) return val;

        var mapList = mapLists[mapName];
        var map = mapList.Maps.SingleOrDefault(x => val.InRange(x.SourceRange));
        val += map?.DestinationOffset ?? 0;

        return ConvertToValue(val, mapLists, mapList.ToType, lastMapName);
    }
    
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
internal record MapList(string FromType, string ToType, IReadOnlyList<Map> Maps);