using static System.Math;

namespace Day08;

internal static class Functions
{
    public static Node ToNode(string nodeString) 
        => new(nodeString[..3], nodeString.Substring(7, 3), nodeString.Substring(12, 3));

    public static int GetStepCountsToNode(
        IReadOnlyDictionary<string, Node> nodes,
        Span<char> instructions,
        string startId) =>
        GetStepCountsToNode(nodes, instructions, nodes[startId], 0);

    private static int GetStepCountsToNode(
        IReadOnlyDictionary<string, Node> nodes,
        Span<char> instructions,
        Node currentNode,
        int stepCount)
    {
        if (currentNode.Id.EndsWith('Z')) return stepCount;

        var instruction = instructions[stepCount % instructions.Length];

        return GetStepCountsToNode(
            nodes,
            instructions,
            nodes[instruction switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right
            }],
            ++stepCount);
    }

    public static long GCD(long i, long j)
    {
        if (i == 0 || j == 0) throw new ArgumentException("Cannot calculate gcd of 0");
        i = Abs(i);
        j = Abs(j);
        return OrderedGCD(Max(i, j), Min(i, j));
    }

    public static long LCM(long i, long j)
        => i is 0 || j is 0 ? 
            throw new ArgumentException("Cannot calculate lcm of 0") :
            Abs((i / GCD(i, j)) * j);

    private static long OrderedGCD(long i, long j)
    {
        long remainder;

        do
        {
            remainder = i % j;
            i = j;
            j = remainder;
        } while (remainder != 0);

        return i;
    }
}

internal record Node(string Id, string Left, string Right);