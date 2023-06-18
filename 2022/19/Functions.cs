namespace _19;

public static class Functions
{
    public static Blueprint ToBlueprint(string input)
    {
        var allInputSplit = input.Split(' ');
        var numbers = new List<int>();

        foreach (var word in allInputSplit)
        {
            if (int.TryParse(word, out var num)) numbers.Add(num);
        }

        return new Blueprint(
            new OreRobotRecipe(numbers[0]),
            new ClayRobotRecipe(numbers[1]),
            new ObsidianRobotRecipe(numbers[2], numbers[3]),
            new GeodeRobotRecipe(numbers[4], numbers[5]));
    }

    public static int FindMaxGeodes(
        State state,
        Blueprint blueprints,
        int currentTurn,
        int maxTurns,
        SkippedProduction skips)
    {
        if (currentTurn == maxTurns) return state.Resources.Geodes;

        var canProduceOreRobot = state.Resources.Ore >= blueprints.OreRobotRecipe.OreCost;
        var canProduceClayRobot = state.Resources.Ore >= blueprints.ClayRobotRecipe.OreCost;
        var canProduceObsidianRobot = state.Resources.Ore >= blueprints.ObsidianRobotRecipe.OreCost &&
                                      state.Resources.Clay >= blueprints.ObsidianRobotRecipe.ClayCost;
        var canProduceGeodeRobot = state.Resources.Ore >= blueprints.GeodeRobotRecipe.OreCost &&
                                   state.Resources.Obsidian >= blueprints.GeodeRobotRecipe.ObsidianCost;

        var produceNothing = FindMaxGeodes(
            state with
            {
                Resources = new Resources(
                    state.Resources.Ore + state.Robots.OreRobots,
                    state.Resources.Clay + state.Robots.ClayRobots,
                    state.Resources.Obsidian + state.Robots.ObsidianRobots,
                    state.Resources.Geodes + state.Robots.GeodeRobots)
            },
            blueprints,
            currentTurn + 1,
            maxTurns,
            new(canProduceOreRobot, canProduceClayRobot, canProduceObsidianRobot, canProduceGeodeRobot));

        var produceOreRobot = 0;
        var produceClayRobot = 0;
        var produceObsidianRobot = 0;
        var produceGeodeRobot = 0;
        
        if (!skips.GeodeRobot && canProduceGeodeRobot)
        {
            return FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - blueprints.GeodeRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots - blueprints.GeodeRobotRecipe.ObsidianCost,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { GeodeRobots = state.Robots.GeodeRobots + 1 }),
                blueprints, currentTurn + 1, maxTurns, SkippedProduction.None);
        }

        if (!skips.OreRobot &&
            canProduceOreRobot &&
            state.Robots.OreRobots < Math.Max(
                Math.Max(
                    Math.Max(blueprints.OreRobotRecipe.OreCost, blueprints.ClayRobotRecipe.OreCost),
                    blueprints.ObsidianRobotRecipe.OreCost),
                blueprints.GeodeRobotRecipe.OreCost))
        {
            produceOreRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - blueprints.OreRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { OreRobots = state.Robots.OreRobots + 1 }),
                blueprints, currentTurn + 1, maxTurns, SkippedProduction.None);
        }
        
        if (!skips.ClayRobot && canProduceClayRobot && state.Robots.ClayRobots < blueprints.ObsidianRobotRecipe.ClayCost)
        {
            produceClayRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - blueprints.ClayRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { ClayRobots = state.Robots.ClayRobots + 1 }),
                blueprints, currentTurn + 1, maxTurns, SkippedProduction.None);
        }
        
        if (!skips.ObsidianRobot && canProduceObsidianRobot)
        {
            produceObsidianRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - blueprints.ObsidianRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots - blueprints.ObsidianRobotRecipe.ClayCost,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { ObsidianRobots = state.Robots.ObsidianRobots + 1 }),
                blueprints, currentTurn + 1, maxTurns, SkippedProduction.None);
        }

        return new[] { produceNothing, produceOreRobot, produceClayRobot, produceObsidianRobot, produceGeodeRobot }
            .Aggregate(Math.Max);
    }
}