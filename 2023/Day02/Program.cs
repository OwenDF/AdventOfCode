using static Day02.Functions;

var games = File.ReadAllLines("Input.txt").Select(ToGame).ToList();

var gameSum = 0;
var (maxRed, maxGreen, maxBlue) = (12, 13, 14);

for (var i = 0; i < games.Count; i++)
{
    foreach (var round in games[i].Rounds)
        if (round.Red > maxRed || round.Green > maxGreen || round.Blue > maxBlue) goto continueOuter;

    gameSum += i + 1;
    continueOuter: ;
}

Console.WriteLine(gameSum);