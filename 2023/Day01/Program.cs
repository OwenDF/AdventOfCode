var lines = File.ReadAllLines("Input.txt");

var total = lines.Select(x => int.Parse(x.First(char.IsAsciiDigit).ToString() + x.Last(char.IsAsciiDigit))).Sum();

Console.WriteLine(total);