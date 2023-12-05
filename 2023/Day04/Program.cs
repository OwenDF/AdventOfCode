using static Day04.Functions;

Console.WriteLine(
    File.ReadAllLines("Input.txt")
        .Select(ToScratchCard)
        .Select(CalculateScore)
        .Sum());