// See https://aka.ms/new-console-template for more information

using _2;
using static _2.Move;
using static _2.Result;

var input = await File.ReadAllLinesAsync("Input.txt");

Console.WriteLine(input
    .Select(x => x.Split(' '))
    .Select(x => new Round(InterpretMove(x[0][0]), InterpretResult(x[1][0])))
    .Aggregate(0, CountPoints));

int CountPoints(int current, Round next)
    => current + (int)ReturnMyMove(next) + (int)next.Result;

Move InterpretMove(char c)
{
    switch (c)
    {
        case 'A':
            return Rock;
        case 'B':
            return Paper;
        case 'C':
            return Scissors;
        default: throw new Exception();
    }
}

Result InterpretResult(char c)
{
    switch (c)
    {
        case 'X':
            return Lose;
        case 'Y':
            return Draw;
        case 'Z':
            return Win;
        default: throw new Exception();
    }
}

Move ReturnMyMove(Round round)
{
    switch (round.Result, round.Theirs)
    {
        case (Win, Scissors):
        case (Lose, Paper):
        case (Draw, Rock):
            return Rock;
        case (Win, Rock):
        case (Lose, Scissors):
        case (Draw, Paper):
            return Paper;
        case (Lose, Rock):
        case (Draw, Scissors):
        case (Win, Paper):
            return Scissors;
        default: throw new Exception();
    }
}
