using static _4.Functions;

var input = await File.ReadAllLinesAsync("Input.txt");

var overlapCount = input.Select(ToElfPair)
    .Where(FullyOverlaps)
    .Count();

var partialOverlapCount = input.Select(ToElfPair)
    .Where(PartiallyOverlaps)
    .Count();
    
Console.WriteLine(overlapCount);
Console.WriteLine(partialOverlapCount);