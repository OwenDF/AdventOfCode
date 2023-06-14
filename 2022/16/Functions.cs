namespace _16;

public static class Functions
{
    public static Valve MapToValve(this string input)
    {
        const string first = "Valve ";
        const string end = "valves ";

        var valveId = input.Substring(first.Length, 2);
        var flowRate = int.Parse(input[(input.IndexOf('=') + 1)..input.IndexOf(';')]);
        string[] adjacents;
        
        if (input.Contains(end))
        {
            adjacents = input.Substring(input.IndexOf(end) + end.Length).Split(", ");
        }
        else
        {
            adjacents = new[] { input[^2..] };
        }

        return new(valveId, flowRate, adjacents);
    }

    public static int FindValveDistance(ValvePair pair, Dictionary<string, Valve> allValves)
    {
        return FindValve("", pair.First, pair.Second, 0, allValves);
    }

    public static int FindValve(
        string previous,
        Valve current,
        Valve target,
        int currentDistance,
        Dictionary<string, Valve> allValves)
    {
        if (currentDistance > 15) return int.MaxValue;
        if (current == target) return currentDistance;

        if (current.AdjacentValves.Count is 1 && current.AdjacentValves[0] == previous) return int.MaxValue;

        return current.AdjacentValves
            .Where(x => x != previous)
            .Select(x => FindValve(current.Name, allValves[x], target, currentDistance + 1, allValves))
            .Min();
    }

    public static int FindBestPressureRelease(List<ValvePairDistance> valveDistances)
    {
        var result = OpenValve(valveDistances, 0, 30, new HashSet<string>(), "AA");

        result.ForEach(x => Console.WriteLine($"Valve: {x.ValveName}, Release: {x.CumulativeRelease}, Time: {x.Time}"));

        return result[0].CumulativeRelease;
    }

    public static List<PressureRelease> OpenValve(
        List<ValvePairDistance> valveDistances,
        int pressureReleased,
        int time,
        HashSet<string> valvesOpened,
        string currentValve)
    {
        var possibleMoves = valveDistances
            .Where(x => x.Pair.First.Name == currentValve)
            .Select(x => new Move(x.Pair.Second, x.Distance))
            .Concat(valveDistances
                .Where(x => x.Pair.Second.Name == currentValve)
                .Select(x => new Move(x.Pair.First, x.Distance)))
            .Where(x => !valvesOpened.Contains(x.Destination.Name))
            .Where(x => x.Distance + 1 < time)
            .ToList();

        var timeCopy = time;
        var bestMoves = possibleMoves
            .OrderByDescending(x => (timeCopy - (x.Distance + 1)) * x.Destination.FlowRate)
            .ToList();

        if (bestMoves.Count is 0) return new List<PressureRelease> { new("END", pressureReleased, time) };
        
        var paths = new List<List<PressureRelease>>();
        foreach (var move in bestMoves.Take(8))
        {
            var valves = new HashSet<string>(valvesOpened) { move.Destination.Name };

            var release = OpenValve(
                valveDistances,
                pressureReleased + (time - (move.Distance + 1)) * move.Destination.FlowRate,
                time - (move.Distance + 1),
                valves,
                move.Destination.Name);

            release.Add(new(currentValve, pressureReleased, time));
            
            paths.Add(release);
        }
        
        return paths.MaxBy(x => x[0].CumulativeRelease);
    }
}