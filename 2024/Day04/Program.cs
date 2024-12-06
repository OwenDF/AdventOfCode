Console.WriteLine(GetXmasCount(AsTwoDimensionalArray(File.ReadAllLines("Input.txt")), 0, 0));

int GetXmasCount(char[,] searchBox, int xPosition, int yPosition)
{
    if (xPosition > searchBox.GetUpperBound(0))
    {
        xPosition = 0;
        yPosition++;
    }

    if (yPosition > searchBox.GetUpperBound(1)) return 0;

    var xmasCount = GetXmasCountAtPosition(searchBox, xPosition, yPosition);

    return xmasCount + GetXmasCount(searchBox, xPosition + 1, yPosition);
}

int GetXmasCountAtPosition(char[,] searchBox, int xPosition, int yPosition)
{
    if (searchBox[xPosition, yPosition] is not 'X') return 0;

    return new[]
    {
        IsXmas(searchBox, xPosition, yPosition, 1, 0),
        IsXmas(searchBox, xPosition, yPosition, 0, 1),
        IsXmas(searchBox, xPosition, yPosition, 1, 1),
        IsXmas(searchBox, xPosition, yPosition, -1, 0),
        IsXmas(searchBox, xPosition, yPosition, 0, -1),
        IsXmas(searchBox, xPosition, yPosition, -1, -1),
        IsXmas(searchBox, xPosition, yPosition, -1, 1),
        IsXmas(searchBox, xPosition, yPosition, 1, -1)
    }.Select(x => x ? 1 : 0).Sum();
}

bool IsXmas(char[,] searchBox, int xPosition, int yPosition, int xTravel, int yTravel)
{
    if (xPosition + xTravel * 3 > searchBox.GetUpperBound(0) || xPosition + xTravel * 3 < 0 ||
        yPosition + yTravel * 3 > searchBox.GetUpperBound(1) || yPosition + yTravel * 3 < 0)
    {
        return false;
    }

    return searchBox[xPosition + xTravel, yPosition + yTravel] is 'M' &&
           searchBox[xPosition + xTravel * 2, yPosition + yTravel * 2] is 'A' &&
           searchBox[xPosition + xTravel * 3, yPosition + yTravel * 3] is 'S';
}

char[,] AsTwoDimensionalArray(string[] lines)
{
    var result = new char[lines.Length, lines[0].Length];
    for (var i = 0; i < lines.Length; i++)
    for (var j = 0; j < lines[0].Length; j++)
        result[i, j] = lines[i][j];

    return result;
}