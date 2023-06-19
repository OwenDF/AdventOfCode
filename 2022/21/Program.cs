using static _21.Functions;

var monkeys = new Dictionary<string, Func<long>>();

(await File.ReadAllLinesAsync("Input.txt")).ToList().ForEach(x => CreateMonkey(monkeys, x));
    
Console.WriteLine(monkeys["root"]());