// See https://aka.ms/new-console-template for more information

var input = await File.ReadAllTextAsync("Input.txt");

var maxCalories = input
    .Split("\n\n", StringSplitOptions.RemoveEmptyEntries)
    .Select(x => x.Split("\n", StringSplitOptions.RemoveEmptyEntries))
    .Select(x => x.Select(int.Parse))
    .Select(x => x.Sum())
    .Max();

Console.WriteLine(maxCalories);
