using _25;

var intSum = (await File.ReadAllLinesAsync("Input.txt")).Select(x => new Snafu(x)).Select(x => x.ToLong()).Sum();

Console.WriteLine(intSum.ToSnafu().Raw);