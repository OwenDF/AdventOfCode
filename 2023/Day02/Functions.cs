namespace Day02;

internal static class Functions
{
    public static Game ToGame(string gameString)
    {
        var roundStrings = gameString.Split(':')[1].Split(';');
        var rounds = roundStrings.Select(ToRound).ToList();

        return new Game(rounds);
    }

    public static Round ToRound(string roundString)
    {
        var rounds = roundString.Split(',').Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();

        return new Round(
            int.Parse(rounds.SingleOrDefault(x => x[1] is "red")?[0] ?? "0"),
            int.Parse(rounds.SingleOrDefault(x => x[1] is "blue")?[0] ?? "0"),
            int.Parse(rounds.SingleOrDefault(x => x[1] is "green")?[0] ?? "0"));
    }
}

public record Round(int Red, int Blue, int Green);
public record Game(IReadOnlyList<Round> Rounds);