using static _5.Functions; 

var input = await File.ReadAllLinesAsync("Input.txt");
var separatorLineIndex = Array.IndexOf(input, string.Empty);
var warehouseInput = input.Take(separatorLineIndex).Reverse().ToArray();

var warehouse = CreateWarehouse(warehouseInput);
var instructions = input[(separatorLineIndex + 1)..].Select(CreateInstruction);

var result = instructions.Aggregate(warehouse, (c, n) => n.Perform(c)).Aggregate("", (c, n) => c + n.Value.Peek().Type);

Console.WriteLine(result);