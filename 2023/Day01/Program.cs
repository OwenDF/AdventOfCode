using static Day01.Functions;

var lines = File.ReadAllLines("Input.txt");

var total = lines.Select(ToDigitLine).Select(x => int.Parse(x[0].ToString() + x[^1])).Sum();

Console.WriteLine(total);