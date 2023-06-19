namespace _21;

public static class Functions
{
    public static void CreateMonkey(Dictionary<string, Func<long>> monkeys, string input)
    {
        var split = input.Split(' ');
        var name = split[0].Substring(0, 4);
        Func<long> func;

        func = int.TryParse(split[1], out var number)
            ? () => number
            : split[2][0] switch
            {
                '*' => () => monkeys[split[1]]() * monkeys[split[3]](),
                '/' => () => monkeys[split[1]]() / monkeys[split[3]](),
                '+' => () => monkeys[split[1]]() + monkeys[split[3]](),
                '-' => () => monkeys[split[1]]() - monkeys[split[3]]()
            };

        monkeys.Add(name, func);
    }

    public static NodePre CreateNodePre(string input) 
    {
        var split = input.Split(' ');
        var name = split[0].Substring(0, 4);

        return int.TryParse(split[1], out var number)
            ? new IntNodePre(name, number)
            : new OperatorNodePre(name, split[1], split[3], split[2][0]);
    }

    public static Node ConstructNode(string nodeName, Dictionary<string, NodePre> nodesInfo)
    {
        if (nodeName is "humn") return new VariableNode();
        if (nodesInfo[nodeName] is IntNodePre inp) return new IntNode(inp.Value);

        if (nodesInfo[nodeName] is OperatorNodePre onp)
            return new OperatorNode(
                ConstructNode(onp.Left, nodesInfo),
                ConstructNode(onp.Right, nodesInfo),
                onp.Operator);

        throw new Exception();
    }

    public static bool ContainsVariable(this Node node) => node switch
    {
        VariableNode => true,
        IntNode => false,
        OperatorNode on => ContainsVariable(on.Left) || ContainsVariable(on.Right)
    };

    public static Node SolveEquation(Node variable, Node solution)
    {
        if (variable is VariableNode) return solution;

        if (variable is OperatorNode on) 
        {
            if (on.Left.ContainsVariable())
            {
                return on.Operator switch
                {
                    '+' => SolveEquation(on.Left, new OperatorNode(solution, on.Right, '-')),
                    '-' => SolveEquation(on.Left, new OperatorNode(solution, on.Right, '+')),
                    '*' => SolveEquation(on.Left, new OperatorNode(solution, on.Right, '/')),
                    '/' => SolveEquation(on.Left, new OperatorNode(solution, on.Right, '*'))
                };
            }

            return on.Operator switch
            {
                '+' => SolveEquation(on.Right, new OperatorNode(solution, on.Left, '-')),
                '-' => SolveEquation(
                    on.Right,
                    new OperatorNode(
                        new OperatorNode(solution, on.Left, '-'),
                        new IntNode(-1), '*')),
                '*' => SolveEquation(on.Right, new OperatorNode(solution, on.Left, '/')),
                '/' => SolveEquation(on.Right, new OperatorNode(on.Left, solution, '/'))
            };
        }

        throw new Exception();
    }

    public static long ResolveNode(Node node) => node switch
    {
        OperatorNode on => on.Operator switch
        {
            '+' => ResolveNode(on.Left) + ResolveNode(on.Right),
            '-' => ResolveNode(on.Left) - ResolveNode(on.Right),
            '*' => ResolveNode(on.Left) * ResolveNode(on.Right),
            '/' => ResolveNode(on.Left) / ResolveNode(on.Right)
        },
        IntNode i => i.Value
    };
}