namespace _21;

public record Node;
public record IntNode(long Value) : Node;
public record OperatorNode(Node Left, Node Right, char Operator) : Node;
public record VariableNode : Node;
public interface NodePre
{
    string Name { get; }
}
public record OperatorNodePre(string Name, string Left, string Right, char Operator) : NodePre;
public record IntNodePre(string Name, int Value) : NodePre;