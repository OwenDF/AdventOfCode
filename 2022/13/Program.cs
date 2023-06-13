using static _13.Functions;

var correctPairs = 0;
var input = (await File.ReadAllLinesAsync("Input.txt")).Chunk(3).Select(ToPairs).ToList();

for (var i = 0; i < input.Count; i++)
{
    if (IsProperlyOrdered(input[i])) correctPairs += i + 1;
}

Console.WriteLine(correctPairs);