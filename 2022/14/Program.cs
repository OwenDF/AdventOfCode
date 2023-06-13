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

var yMax = blockers.MaxBy(x => x.Y).Y + 1;
var sandStart = new Point(500, 0);

int sandCount;
for (var i = 0;;i++)
{
    if (blockers.Contains(sandStart))
    {
        sandCount = i;
        break;
    }

    var sandParticle = sandStart;
    while (true)
    {
        if (sandParticle.Y == yMax)
        {
            blockers.Add(sandParticle);
            break;
        }

        var down = sandParticle with { Y = sandParticle.Y + 1 };
        var downLeft = new Point(sandParticle.X - 1, sandParticle.Y + 1);
        var downRight = new Point(sandParticle.X + 1, sandParticle.Y + 1);

        if (!blockers.Contains(down))
        {
            sandParticle = down;
            continue;
        }

        if (!blockers.Contains(downLeft))
        {
            sandParticle = downLeft;
            continue;
        }
        
        if (!blockers.Contains(downRight))
        {
            sandParticle = downRight;
            continue;
        }
        
        blockers.Add(sandParticle);
        break;
    }
}

Console.WriteLine(sandCount);
