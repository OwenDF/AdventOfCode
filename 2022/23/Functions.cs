namespace _23;
using static Direction;

public static class Functions
{
    public static IReadOnlySet<Point> ParseInitialElfPositions(string[] input)
    {
        var elves = new HashSet<Point>();

        for (var j = 0; j < input.Length; j++)
        for(var i = 0; i < input[j].Length; i++)
            if (input[j][i] is '#')
                elves.Add(new(i, j));

        return elves;
    }

    public static IReadOnlySet<Point> DisperseElves(
        IReadOnlySet<Point> startingElves,
        IReadOnlyList<Direction> orderOfDirections)
    {
        var proposals = new Dictionary<Point, Point>();
        var conflictedProposals = new HashSet<Point>();
        var isolatedElves = 0;
        foreach (var elf in startingElves)
        {
            var elfContext = CreateElfContext(elf);
            if (elfContext.IsIsolated(startingElves)) continue;

            var proposedMove = ProposeMove(elfContext, startingElves, orderOfDirections);

            if (proposals.ContainsKey(proposedMove))
            {
                conflictedProposals.Add(proposedMove);
                continue;
            }

            proposals.Add(proposedMove, elf);
        }

        foreach (var conflict in conflictedProposals) proposals.Remove(conflict);

        var elves = new HashSet<Point>(startingElves);

        foreach (var proposal in proposals)
        {
            elves.Remove(proposal.Value);
            elves.Add(proposal.Key);
        }

        return elves;
    }

    private static Point ProposeMove(
        ElfContext elf,
        IReadOnlySet<Point> elves,
        IReadOnlyList<Direction> orderOfDirections) 
    {
        foreach (var direction in orderOfDirections)
        {
            var canMoveInDirection = direction switch
            {
                N => elf.CanMoveNorth(elves),
                E => elf.CanMoveEast(elves),
                S => elf.CanMoveSouth(elves),
                W => elf.CanMoveWest(elves)
            };

            if (canMoveInDirection)
            {
                return direction switch
                {
                    N => elf.N,
                    E => elf.E,
                    S => elf.S,
                    W => elf.W
                };
            }
        }

        return elf.C;
    }

    private static ElfContext CreateElfContext(Point elf)
    {
        var (x, y) = elf;

        return new ElfContext(new (x, y), new(x, y - 1), new(x + 1, y - 1), new(x + 1, y), new(x + 1, y + 1),
            new(x, y + 1), new(x - 1, y + 1), new(x - 1, y), new(x - 1, y - 1));
    }
}