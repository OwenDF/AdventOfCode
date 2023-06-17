namespace _19;

public record OreRobotRecipe(int OreCost);
public record ClayRobotRecipe(int OreCost);
public record ObsidianRobotRecipe(int OreCost, int ClayCost);
public record GeodeRobotRecipe(int OreCost, int ObsidianCost);

public record Blueprint(
    OreRobotRecipe OreRobotRecipe,
    ClayRobotRecipe ClayRobotRecipe,
    ObsidianRobotRecipe ObsidianRobotRecipe,
    GeodeRobotRecipe GeodeRobotRecipe);

public record Resources(int Ore, int Clay, int Obsidian, int Geodes);
public record Robots(int OreRobots, int ClayRobots, int ObsidianRobots, int GeodeRobots);
public record State(Resources Resources, Robots Robots);
public record SkippedProduction(bool OreRobot, bool ClayRobot, bool ObsidianRobot, bool GeodeRobot)
{
    public static readonly SkippedProduction None = new(false, false, false, false);
}