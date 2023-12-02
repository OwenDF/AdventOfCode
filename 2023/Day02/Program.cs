using static Day02.Functions;

Console.WriteLine(
    (from game in File.ReadAllLines("Input.txt").Select(ToGame) 
    let minBlue = game.Rounds.Max(x => x.Blue)
    let minRed = game.Rounds.Max(x => x.Red)
    let minGreen = game.Rounds.Max(x => x.Green)
    select minBlue * minGreen * minRed).Sum());