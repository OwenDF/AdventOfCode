var input = await File.ReadAllTextAsync("Input.txt");

int startOfPacket;
int startOfMessage;

for (var i = 3;; i++)
{
    var set = new HashSet<char> { input[i], input[i - 1], input[i - 2], input[i - 3] };
    if (set.Count is 4)
    {
        startOfPacket = i + 1;
        break;
    }
}

for (var i = 13;; i++)
{
    var set = new HashSet<char>(input[(i-13)..(i+1)]);
    if (set.Count is 14)
    {
        startOfMessage = i + 1;
        break;
    }
}

Console.WriteLine(startOfPacket);
Console.WriteLine(startOfMessage);