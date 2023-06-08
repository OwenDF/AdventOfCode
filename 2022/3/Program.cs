using static _3.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");
var partOneResult = input.Select(ToRucksackContents)
    .Select(GetCommonChar)
    .Select(ConvertToPriorityValue)
    .Sum();

Console.WriteLine(partOneResult);

var partTwoResult = input.Select(ToRucksackContents)
    .Chunk(3)
    .Select(GetCommonChar)
    .Select(ConvertToPriorityValue)
    .Sum();

Console.WriteLine(partTwoResult);
