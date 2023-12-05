using static Day04.Functions;

var scores = File.ReadAllLines("Input.txt")
    .Select(ToScratchCard)
    .Select(CalculateScore).ToArray();

var cardCounts = scores.Select(_ => 1).ToArray();

for (var i = 0; i < cardCounts.Length; i++)
{
    var cardScore = scores[i];
    var cardCount = cardCounts[i];
    for (var j = i + 1; j < cardCounts.Length && j <= i + cardScore; j++)
    {
        cardCounts[j] += cardCount;
    }
}

Console.WriteLine(cardCounts.Sum());