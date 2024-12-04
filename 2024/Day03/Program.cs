var textSpan = File.ReadAllText("Input.txt").AsSpan();

long sum = 0;
while (true)
{
    var possibleInstructionStart = textSpan.IndexOf("mul(");
    if (possibleInstructionStart is -1) break;

    textSpan = textSpan[(possibleInstructionStart + 4)..];

    int firstPart;
    if (int.TryParse(textSpan[..3], out var num3))
    {
        firstPart = num3;
        textSpan = textSpan[3..];
    }
    else if (int.TryParse(textSpan[..2], out var num2))
    {
        firstPart = num2;
        textSpan = textSpan[2..];
    }
    else if (int.TryParse(textSpan[..1], out var num1))
    {
        firstPart = num1;
        textSpan = textSpan[1..];
    }
    else continue;

    if (textSpan[0] is not ',') continue;
    
    textSpan = textSpan[1..];
    
    int secondPart;
    if (int.TryParse(textSpan[..3], out var num23))
    {
        secondPart = num23;
        textSpan = textSpan[3..];
    }
    else if (int.TryParse(textSpan[..2], out var num22))
    {
        secondPart = num22;
        textSpan = textSpan[2..];
    }
    else if (int.TryParse(textSpan[..1], out var num21))
    {
        secondPart = num21;
        textSpan = textSpan[1..];
    }
    else continue;
    
    if (textSpan[0] is ')') sum += firstPart * secondPart;
}

Console.WriteLine(sum);