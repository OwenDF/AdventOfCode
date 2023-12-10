using Day06;
using static Day06.Functions;

// var races = new Race[] { new(7,9), new(15,40), new(30,200) };
var races = new Race[] { new(34,204), new(90,1713), new(89,1210), new(86,1780) };

var total = races.Select(CalculateRaceCombinationTotal).Aggregate((c, n) => c * n);

Console.WriteLine(total);