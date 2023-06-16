namespace _18;

public static class Functions
{
    public static Point ToPoint(string input)
    {
        var split = input.Split(',');
        return new (int.Parse(split[0]), int.Parse(split[1]), int.Parse(split[2]));
    }

    public static HashSet<Point> FloodFill(HashSet<Point> shapePoints, Point startPoint, Point minBound, Point maxBound)
    {
        var workingSet = new HashSet<Point>();
        if (FloodFillRecursive(shapePoints, workingSet, startPoint, minBound, maxBound))
        {
            workingSet.UnionWith(shapePoints);
            return workingSet;
        }

        return shapePoints;
    }
    
    public static Point[] GetAllAdjacentPoints(this Point centre)
    {
        return new[]
        {
            centre with { X = centre.X - 1 },
            centre with { Y = centre.Y - 1 },
            centre with { Z = centre.Z - 1 },
            centre with { X = centre.X + 1 },
            centre with { Y = centre.Y + 1 },
            centre with { Z = centre.Z + 1 },
        };
    }

    private static bool FloodFillRecursive(
        HashSet<Point> shapePoints,
        HashSet<Point> innerPoints,
        Point currentPoint,
        Point minBound,
        Point maxBound)
    {
        if (currentPoint == new Point(2, 2, 5))
        {
            var bla = 1;
        }

        if (shapePoints.Contains(currentPoint) || innerPoints.Contains(currentPoint)) return true;

        if (currentPoint.X <= minBound.X || currentPoint.X >= maxBound.X || currentPoint.Y <= minBound.Y ||
            currentPoint.Y >= maxBound.Y || currentPoint.Z <= minBound.Z || currentPoint.Z >= maxBound.Z)
            return false;

        innerPoints.Add(currentPoint);

        return currentPoint.GetAllAdjacentPoints()
            .All(x => FloodFillRecursive(shapePoints, innerPoints, x, minBound, maxBound));
    }
}