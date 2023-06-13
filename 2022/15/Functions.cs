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
}