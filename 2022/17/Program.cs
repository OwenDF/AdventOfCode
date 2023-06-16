using _17;

var jetDirections = (await File.ReadAllTextAsync("Input.txt")).Trim().Select(x => x).ToArray();

const long maxRocks = 1_000_000_000_000;

var cave = new HashSet<Point> { new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0) };
var rockFactory = new RockFactory();
var rockNumber = 0;
var maxHeight = 0;
var rock = rockFactory.GetNextRock(cave, 4, rockNumber);
int magicNumber;

for (var turnCount = 0L; rockNumber < 7000; turnCount++)
{
    var jetDirection = jetDirections[turnCount % jetDirections.Length];

    switch (jetDirection)
    {
        case '<':
            rock.MoveLeft();
            break;
        case '>':
            rock.MoveRight();
            break;
        default:
            throw new Exception($"Character: {jetDirection}");
    }

    if (rock.GetFullCurrentPosition().Any(x => cave.Contains(x))) throw new Exception();

    var dropResult = rock.Drop();
    if (dropResult is not null)
    {
        maxHeight = Math.Max(dropResult.Value, maxHeight);
        // maxHeights.Add(maxHeight);
        // if ((rockNumber + 1) > 100 && (rockNumber + 1) % 10 == 0 && maxHeights[(rockNumber + 1) / 2] * 2 == maxHeight)
        // {
        //     Console.WriteLine(rockNumber + 1);
        //
        //     if (maxRocks % (rockNumber + 1) == 0)
        //     {
        //         Console.WriteLine((maxRocks / (rockNumber + 1)) * maxHeight);
        //         break;
        //     }
        // }

        // if ((rockNumber + 1) % 17000 == 0) Console.WriteLine(maxHeight);
        // if ((rockNumber + 1) % 170000 == 0) break;

        rock = rockFactory.GetNextRock(cave, maxHeight + 4, ++rockNumber);
    }

    // if (turnCount + 1 % jetDirections.Length == 0 && cave.Contains(new(0, maxHeight)) &&
    //     cave.Contains(new(1, maxHeight)) && cave.Contains(new(2, maxHeight)) && cave.Contains(new(3, maxHeight)) &&
    //     cave.Contains(new(4, maxHeight)) && cave.Contains(new(5, maxHeight)) && cave.Contains(new(6, maxHeight)))
    // {
    //     Console.WriteLine(rockNumber);
    // }

    // if (turnCount + 1 % jetDirections.Length == 0 && rockNumber % 5 == 0)
    // {
    //     Console.WriteLine(rockNumber);
    //     Console.WriteLine(maxHeight);
    //     break;
    // }
}

Console.WriteLine(maxRocks % 17_000);
Console.WriteLine(maxHeight);

Console.WriteLine(((maxRocks / 17_000) * 26540) + maxHeight);

