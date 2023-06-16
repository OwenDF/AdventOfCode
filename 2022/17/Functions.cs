namespace _17;

public static class Functions
{
    public static void AddNew<T>(this HashSet<T> set, T item)
    {
        if (!set.Add(item)) throw new Exception();
    }
}