using _13;
using static _13.Functions;

var correctPairs = 0;
var input = await File.ReadAllLinesAsync("Input.txt");
var pairs = input.Chunk(3).Select(ToPairs).ToList();

for (var i = 0; i < pairs.Count; i++)
{
    if (IsProperlyOrdered(pairs[i])) correctPairs += i + 1;
}

Console.WriteLine(correctPairs);

var dividerOne = "[[2]]".AsSpan().CreateElement();
var dividerTwo = "[[6]]".AsSpan().CreateElement();

var packets = input.Where(x => !string.IsNullOrEmpty(x))
    .Select(x => x.AsSpan().CreateElement())
    .Append(dividerOne)
    .Append(dividerTwo)
    .OrderBy(x => x, new PacketComparer())
    .ToList();

Console.WriteLine((packets.IndexOf(dividerOne) + 1) * (packets.IndexOf(dividerTwo) + 1));