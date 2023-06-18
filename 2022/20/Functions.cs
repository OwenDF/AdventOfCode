namespace _20;

public static class Functions
{
    public static LinkedListNode<T> GetPrevious<T>(this LinkedListNode<T> current, int count) 
    {
        for (var i = 0; i < count; i++)
        {
            current = current.Previous ?? current.List!.Last!;
        }

        return current;
    }
    
    public static LinkedListNode<T> GetNext<T>(this LinkedListNode<T> current, int count) 
    {
        for (var i = 0; i < count; i++)
        {
            current = current.Next ?? current.List!.First!;
        }

        return current;
    }
}