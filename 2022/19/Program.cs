using _19;
using static _19.Functions;

var recipes = (await File.ReadAllLinesAsync("Input.txt")).Select(ToBlueprint).ToList();
var initialState = new State(new Resources(0, 0, 0, 0), new Robots(1, 0, 0, 0));

var totalQuality = 0;
for (var i = 0; i < recipes.Count; i++)
{
    totalQuality += (i + 1) * FindMaxGeodes(initialState, recipes[i], 0, 24, SkippedProduction.None);
}

var score = 1;
foreach (var recipe in recipes.Take(3))
{
    score *= FindMaxGeodes(initialState, recipe, 0, 32, SkippedProduction.None);
}

Console.WriteLine(totalQuality);
Console.WriteLine(score);
