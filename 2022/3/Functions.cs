namespace _3;

public static class Functions
{
    public static Rucksack ToRucksackContents(string contents) => 
        new(contents[..(contents.Length / 2)].ToCharArray().ToHashSet(),
            contents[(contents.Length / 2)..].ToCharArray().ToHashSet());

    public static char GetCommonChar(Rucksack rucksack) =>
        rucksack.FirstCompartment.Intersect(rucksack.SecondCompartment).Single();

    public static char GetCommonChar(IEnumerable<Rucksack> rucksacks) =>
        rucksacks.Select(x => x.FirstCompartment.Union(x.SecondCompartment))
            .Aggregate((c, n) => c.Intersect(n))
            .Single();

    public static int ConvertToPriorityValue(char character) =>
        char.IsUpper(character) ? character - 38 : character - 96;
}