namespace _15;

public static class Functions
{
    public static SensorBeaconPair MapSensorBeaconPair(this string info)
    {
        const string sensorAt = "Sensor at ";
        const string beaconAt = ": closest beacon is at ";
        const string sep = ", ";

        var sensorString = info.Substring(sensorAt.Length, info.IndexOf(':') - sensorAt.Length).Split(sep);
        var sensor = new Point(Read(sensorString[0]), Read(sensorString[1]));
        var beaconString = info[(info.IndexOf(beaconAt, StringComparison.Ordinal) + beaconAt.Length)..].Split(sep);
        var beacon = new Point(Read(beaconString[0]), Read(beaconString[1]));

        return new(sensor, beacon);
    }

    public static int GetDistance(Point first, Point second) =>
        Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);

    private static int Read(string num) =>
        int.Parse(new string(num.AsSpan()[2..]));

    public static Point FindUncoveredPoint(List<SensorBeaconPair> pairs) 
    {
        const int magicMax = 4_000_000;

        for (var y = 0; y <= magicMax; y++)
        {
            var rowCoveredRanges = pairs.Select(p => GetRowCoverage(p, y))
                .Where(x => x is not null)
                .Select(x => x!.Value)
                .ToList();
            
            var nextPosition = FindNextOpenPointOnRow(rowCoveredRanges, 0);

            if (nextPosition <= magicMax) return new Point(nextPosition, y);
        }

        throw new Exception();
    }

    private static CoverageRange? GetRowCoverage(SensorBeaconPair pair, int rowNum)
    {
        var yElement = Math.Abs(pair.SensorLocation.Y - rowNum);
        var range = GetDistance(pair.SensorLocation, pair.BeaconLocation);

        if (range < yElement) return null;

        return new CoverageRange(
            pair.SensorLocation.X - (range - yElement),
            pair.SensorLocation.X + (range - yElement));
    }

    private static int FindNextOpenPointOnRow(List<CoverageRange> ranges, int currentPosition)
    {
        var relevantRanges = ranges.Where(x => currentPosition >= x.MinIncl && currentPosition <= x.MaxIncl).ToList();

        if (relevantRanges.Count is 0) return currentPosition;

        var nextPosition = relevantRanges.MaxBy(x => x.MaxIncl).MaxIncl + 1;
        return FindNextOpenPointOnRow(ranges, nextPosition);
    }
}