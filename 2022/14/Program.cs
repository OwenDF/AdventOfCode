using _14;
using static _14.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");
var blockers = new HashSet<Point>();

foreach (var inputLine in input)
{
    var linePoints = inputLine.Split("->").Select(x => x.Trim()).Select(x => x.ToPoint()).ToList();
    blockers.Add(linePoints[0]);

    for (var i = 1; i < linePoints.Count; i++)
    {
        var (previous, current) = (linePoints[i - 1], linePoints[i]);
        AddBlockers(blockers, previous, current);
    }
}

Console.WriteLine(GetTotalSandfall(blockers));
