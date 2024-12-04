var textSpan = File.ReadAllText("Input.txt").AsSpan();

long sum = 0;
var on = true;
while (true)
{
    var possibleMulStart = textSpan.IndexOf("mul(");
    var doStart = textSpan.IndexOf("do()");
    var dontStart = textSpan.IndexOf("don't()");
    if (possibleMulStart is -1) break;

    if (doStart is not - 1 && (doStart < dontStart || dontStart is -1) && doStart < possibleMulStart)
    {
        textSpan = textSpan[(doStart + 4)..];
        on = true;
        continue;
    }
    
    if (dontStart is not -1 && dontStart < possibleMulStart)
    {
        textSpan = textSpan[(dontStart + 6)..];
        on = false;
        continue;
    }

    textSpan = textSpan[(possibleMulStart + 4)..];
    if (!on) continue;

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