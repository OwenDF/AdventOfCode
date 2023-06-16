using _17;

var jetDirections = (await File.ReadAllTextAsync("Input.txt")).Trim().Select(x => x).ToArray();

var cave = new HashSet<Point> { new(0, 0), new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0) };
var rockFactory = new RockFactory();
var rockNumber = 0;
var maxHeight = 0;
var rock = rockFactory.GetNextRock(cave, 4, rockNumber);
var counter = 0;

// Console.WriteLine(jetDirections.Length);
// Console.WriteLine(new string(jetDirections));

for (var turnCount = 0; rockNumber < 2022; turnCount++)
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
    
    // for (var y = 20; y >= 0; y--)
    // {
    //     Console.Write($"{y:0000}: ");
    //     for (var x = 0; x < 7; x++)
    //     {
    //         if (cave.Contains(new(x, y))) Console.Write("#");
    //         else if (rock.GetFullCurrentPosition().Contains(new(x, y))) Console.Write("@");
    //         else Console.Write(".");
    //     }
    //
    //     Console.WriteLine();
    // }
    
    // Console.WriteLine();Console.WriteLine();

    var dropResult = rock.Drop();
    if (dropResult is not null)
    {
        counter++;
        maxHeight = Math.Max(dropResult.Value, maxHeight);
        rock = rockFactory.GetNextRock(cave, maxHeight + 4, ++rockNumber);
    }
    
    // for (var y = 20; y >= 0; y--)
    // {
    //     Console.Write($"{y:0000}: ");
    //     for (var x = 0; x < 7; x++)
    //     {
    //         if (cave.Contains(new(x, y))) Console.Write("#");
    //         else if (rock.GetFullCurrentPosition().Contains(new(x, y))) Console.Write("@");
    //         else Console.Write(".");
    //     }
    //
    //     Console.WriteLine();
    // }
    //
    // Console.WriteLine();Console.WriteLine();
    //
    // Console.WriteLine();
    // Console.WriteLine();
    // Console.WriteLine(maxHeight);
    // Console.WriteLine();
    // Console.WriteLine();


    // if (turnCount % 100 == 0) Console.WriteLine(turnCount);
}

// for (var y = 20; y >= 0; y--)
// {
//     Console.Write($"{y:0000}: ");
//     for (var x = 0; x < 7; x++)
//     {
//         Console.Write(cave.Contains(new(x, y)) ? "#" : ".");
//     }
//
//     Console.WriteLine();
// }

// foreach (var item in cave)
// {
//     if (item.X is < 0 or > 6) throw new Exception();
// }

Console.WriteLine(maxHeight);

Console.WriteLine(counter);