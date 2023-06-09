using static _4.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");

var overlapCount = input.Select(ToElfPair)
    .Where(FullyOverlaps)
    .Count();
    
Console.WriteLine(overlapCount);