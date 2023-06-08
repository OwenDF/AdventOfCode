using static _3.Functions;

var result = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(ToRucksackContents)
    .Select(GetCommonChar)
    .Select(ConvertToPriorityValue)
    .Sum();

Console.WriteLine(result);
