namespace _15;

public readonly record struct Point(int X, int Y);
public readonly record struct Sensor(Point Location, int Range);
public record SensorBeaconPair(Point SensorLocation, Point BeaconLocation);