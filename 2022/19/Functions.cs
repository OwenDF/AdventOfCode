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
        Blueprint robotRecipes,
        int currentTurn,
        int maxTurns,
        SkippedProduction skips)
    {
        if (currentTurn == maxTurns) return state.Resources.Geodes;

        var canProduceOreRobot = state.Resources.Ore >= robotRecipes.OreRobotRecipe.OreCost;
        var canProduceClayRobot = state.Resources.Ore >= robotRecipes.ClayRobotRecipe.OreCost;
        var canProduceObsidianRobot = state.Resources.Ore >= robotRecipes.ObsidianRobotRecipe.OreCost &&
                                      state.Resources.Clay >= robotRecipes.ObsidianRobotRecipe.ClayCost;
        var canProduceGeodeRobot = state.Resources.Ore >= robotRecipes.GeodeRobotRecipe.OreCost &&
                                   state.Resources.Obsidian >= robotRecipes.GeodeRobotRecipe.ObsidianCost;

        var produceNothing = FindMaxGeodes(
            state with
            {
                Resources = new Resources(
                    state.Resources.Ore + state.Robots.OreRobots,
                    state.Resources.Clay + state.Robots.ClayRobots,
                    state.Resources.Obsidian + state.Robots.ObsidianRobots,
                    state.Resources.Geodes + state.Robots.GeodeRobots)
            },
            robotRecipes,
            currentTurn + 1,
            maxTurns,
            new(canProduceOreRobot, canProduceClayRobot, canProduceObsidianRobot, canProduceGeodeRobot));

        var produceOreRobot = 0;
        var produceClayRobot = 0;
        var produceObsidianRobot = 0;
        var produceGeodeRobot = 0;

        if (!skips.OreRobot && canProduceOreRobot)
        {
            produceOreRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - robotRecipes.OreRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { OreRobots = state.Robots.OreRobots + 1 }),
                robotRecipes, currentTurn + 1, maxTurns, SkippedProduction.None);
        }
        
        if (!skips.ClayRobot && canProduceClayRobot)
        {
            produceClayRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - robotRecipes.ClayRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { ClayRobots = state.Robots.ClayRobots + 1 }),
                robotRecipes, currentTurn + 1, maxTurns, SkippedProduction.None);
        }
        
        if (!skips.ObsidianRobot && canProduceObsidianRobot)
        {
            produceObsidianRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - robotRecipes.ObsidianRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots - robotRecipes.ObsidianRobotRecipe.ClayCost,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { ObsidianRobots = state.Robots.ObsidianRobots + 1 }),
                robotRecipes, currentTurn + 1, maxTurns, SkippedProduction.None);
        }
        
        if (!skips.GeodeRobot && canProduceGeodeRobot)
        {
            produceGeodeRobot = FindMaxGeodes(
                new State(new Resources(
                        state.Resources.Ore + state.Robots.OreRobots - robotRecipes.GeodeRobotRecipe.OreCost,
                        state.Resources.Clay + state.Robots.ClayRobots,
                        state.Resources.Obsidian + state.Robots.ObsidianRobots - robotRecipes.GeodeRobotRecipe.ObsidianCost,
                        state.Resources.Geodes + state.Robots.GeodeRobots),
                    state.Robots with { GeodeRobots = state.Robots.GeodeRobots + 1 }),
                robotRecipes, currentTurn + 1, maxTurns, SkippedProduction.None);
        }

        return new[] { produceNothing, produceOreRobot, produceClayRobot, produceObsidianRobot, produceGeodeRobot }
            .Aggregate(Math.Max);
    }
}