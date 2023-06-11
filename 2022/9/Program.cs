using _9;
using static _9.Functions;

const char Up = 'U';
const char Down = 'D';
const char Right = 'R';
const char Left = 'L';

var instructions = (await File.ReadAllLinesAsync("Input.txt")).Select(MapToInstruction);

var tailVisits = new HashSet<Point>();
var head = new Point(1, 1);
var tail = new Point(1, 1);

foreach (var instruction in instructions)
{
    for (var i = 0; i < instruction.MoveCount; i++)
    {
        switch (instruction.Direction)
        {
            case Up:
                head = head with { Y = head.Y + 1 };
                break;
            case Down:
                head = head with { Y = head.Y - 1 };
                break;
            case Left:
                head = head with { X = head.X - 1 };
                break;
            case Right:
                head = head with { X = head.X + 1 };
                break;
        }
        
        tailVisits.Add(tail);
        if (tail.IsAdjacentTo(head)) continue;
        
        if (tail.X == head.X)
        {
            if (tail.Y < head.Y)
            {
                tail = tail with { Y = tail.Y + 1 };
                continue;
            }
        
            tail = tail with { Y = tail.Y - 1 };
            continue;
        }
        
        if (tail.Y == head.Y)
        {
            if (tail.X < head.X)
            {
                tail = tail with { X = tail.X + 1 };
                continue;
            }
        
            tail = tail with { X = tail.X - 1 };
            continue;
        }
        
        if (tail.Y < head.Y && tail.X < head.X)
        {
            tail = tail with { X = tail.X + 1, Y = tail.Y + 1 };
            continue;
        }
        
        if (tail.Y < head.Y && tail.X > head.X)
        {
            tail = tail with { X = tail.X - 1, Y = tail.Y + 1 };
            continue;
        }
        
        if (tail.Y > head.Y && tail.X > head.X)
        {
            tail = tail with { X = tail.X - 1, Y = tail.Y - 1 };
            continue;
        }
        
        tail = tail with { X = tail.X + 1, Y = tail.Y - 1 };
    }
}

tailVisits.Add(tail);

Console.WriteLine(tailVisits.Count);