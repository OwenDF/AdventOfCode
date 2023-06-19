using _21;
using static _21.Functions;

var monkeys = new Dictionary<string, Func<long>>();

(await File.ReadAllLinesAsync("Input.txt")).ToList().ForEach(x => CreateMonkey(monkeys, x));

var nodePreambles = (await File.ReadAllLinesAsync("Input.txt"))
    .Select(CreateNodePre)
    .Where(x => x.Name != "root")
    .ToDictionary(x => x.Name, x => x);

var rjmz = ConstructNode("rjmz", nodePreambles);
var nfct = ConstructNode("nfct", nodePreambles);

Node variableNode;
Node solutionNode;

if (rjmz.ContainsVariable())
{
    variableNode = rjmz;
    solutionNode = nfct;
}
else
{
    variableNode = nfct;
    solutionNode = rjmz;
}

solutionNode = SolveEquation(variableNode, solutionNode);

Console.WriteLine(ResolveNode(solutionNode));