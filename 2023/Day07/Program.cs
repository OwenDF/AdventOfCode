using static Day07.Functions;

var orderedHands = File.ReadAllLines("Input.txt").Select(ToCardHand).OrderBy(x => x.Cards).ToList();

var score = 0;

for (var i = 0; i < orderedHands.Count; i++) score += (i + 1) * orderedHands[i].Bid;

Console.WriteLine(score);