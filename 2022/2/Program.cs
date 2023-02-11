// See https://aka.ms/new-console-template for more information

using _2;
using static _2.Move;

var input = await File.ReadAllLinesAsync("Input.txt");

Console.WriteLine(input
    .Select(x => x.Split(' '))
    .Select(x => new Round(InterpretMove(x[0][0]), InterpretMove(x[1][0])))
    .Aggregate(0, CountPoints));

int CountPoints(int current, Round next)
    => current + (int)next.Mine + ReturnWinLoseDrawPoints(next);

Move InterpretMove(char c)
{
    switch (c)
    {
        case 'A':
        case 'X':
            return Rock;
        case 'B':
        case 'Y':
            return Paper;
        case 'C':
        case 'Z':
            return Scissors;
        default: throw new Exception();
    }
}

int ReturnWinLoseDrawPoints(Round round)
{
    switch (round.Mine, round.Theirs)
    {
        case (Rock, Scissors):
        case (Scissors, Paper):
        case (Paper, Rock):
            return 6;
        case (Rock, Rock):
        case (Scissors, Scissors):
        case (Paper, Paper):
            return 3;
        case (Scissors, Rock):
        case (Paper, Scissors):
        case (Rock, Paper):
            return 0;
        default: throw new Exception();
    }
}
