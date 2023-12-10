namespace Day08;

internal static class Functions
{
    public static Node ToNode(string nodeString) 
        => new(nodeString[..3], nodeString.Substring(7, 3), nodeString.Substring(12, 3));

    public static int GetStepCountsToNode(
        IReadOnlyDictionary<string, Node> nodes,
        Span<char> instructions,
        string startId,
        string endId) =>
        GetStepCountsToNode(nodes, instructions, nodes[startId], endId, 0);

    private static int GetStepCountsToNode(
        IReadOnlyDictionary<string, Node> nodes,
        Span<char> instructions,
        Node currentNode,
        string endId,
        int stepCount)
    {
        if (currentNode.Id == endId) return stepCount;

        var instruction = instructions[stepCount % instructions.Length];

        return GetStepCountsToNode(
            nodes,
            instructions,
            nodes[instruction switch
            {
                'L' => currentNode.Left,
                'R' => currentNode.Right
            }],
            endId,
            ++stepCount);

    }
}

internal record Node(string Id, string Left, string Right);