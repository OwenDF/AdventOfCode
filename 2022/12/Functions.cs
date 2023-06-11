namespace _12;

public static class Functions
{
    public static void FindMinimumDistances(
        HashSet<WeightedNode> pointsToVisit,
        Dictionary<Point, int> allNodes,
        HashSet<Point> visitedNodes,
        int[,] heights,
        int maxX,
        int maxY)
    {
        var node = pointsToVisit.MinBy(i => i.Weight);
        visitedNodes.Add(node.Location);
        pointsToVisit.Remove(node);        
        var (x, y) = node.Location;

        if (y is not 0 && heights[x,y-1] - 1 <= heights[x,y])
        {
            var south = new Point(x, y - 1);
            var southWeight = Math.Min(allNodes[south], node.Weight + 1);
            allNodes[south] = southWeight;
            if (!visitedNodes.Contains(south)) pointsToVisit.Add(new(south, southWeight));
        }
        
        if (y != maxY - 1 && heights[x,y+1] - 1 <= heights[x,y])
        {
            var north = new Point(x, y + 1);
            var northWeight = Math.Min(allNodes[north], node.Weight + 1);
            allNodes[north] = northWeight;
            if (!visitedNodes.Contains(north)) pointsToVisit.Add(new(north, northWeight));
        }
        
        if (x is not 0 && heights[x-1,y] - 1 <= heights[x,y])
        {
            var west = new Point(x - 1, y);
            var westWeight = Math.Min(allNodes[west], node.Weight + 1);
            allNodes[west] = westWeight;
            if (!visitedNodes.Contains(west)) pointsToVisit.Add(new(west, westWeight));
        }
        
        if (x != maxX - 1 && heights[x+1,y] - 1 <= heights[x,y])
        {
            var east = new Point(x + 1, y);
            var eastWeight = Math.Min(allNodes[east], node.Weight + 1);
            allNodes[east] = eastWeight;
            if (!visitedNodes.Contains(east)) pointsToVisit.Add(new(east, eastWeight));
        }

        if (pointsToVisit.Count is not 0)
            FindMinimumDistances(pointsToVisit, allNodes, visitedNodes, heights, maxX, maxY);
    }
}