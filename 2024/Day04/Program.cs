Console.WriteLine(GetXmasCount(AsTwoDimensionalArray(File.ReadAllLines("Input.txt")), 1, 1));

int GetXmasCount(char[,] searchBox, int xPosition, int yPosition)
{
    if (xPosition > searchBox.GetUpperBound(0) - 1)
    {
        xPosition = 1;
        yPosition++;
    }

    if (yPosition > searchBox.GetUpperBound(1) - 1) return 0;

    return GetXmasCount(searchBox, xPosition + 1, yPosition) +
           (IsXmasAtPosition(searchBox, xPosition, yPosition) ? 1 : 0);
}

bool IsXmasAtPosition(char[,] searchBox, int x, int y)
{
    if (searchBox[x, y] is not 'A') return false;

    return ((searchBox[x - 1, y + 1] is 'M' && searchBox[x + 1, y - 1] is 'S') ||
            (searchBox[x - 1, y + 1] is 'S' && searchBox[x + 1, y - 1] is 'M')) &&
           ((searchBox[x + 1, y + 1] is 'M' && searchBox[x - 1, y - 1] is 'S') ||
            (searchBox[x + 1, y + 1] is 'S' && searchBox[x - 1, y - 1] is 'M'));
}

char[,] AsTwoDimensionalArray(string[] lines)
{
    var result = new char[lines.Length, lines[0].Length];
    for (var i = 0; i < lines.Length; i++)
    for (var j = 0; j < lines[0].Length; j++)
        result[i, j] = lines[i][j];

    return result;
}