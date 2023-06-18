using _20;

const int key = 811589153;
var nums = new LinkedList<long>((await File.ReadAllLinesAsync("Input.txt")).Select(x => (long)int.Parse(x) * key));

var itemCount = nums.Count;

var organisingNode = nums.First;
var nodeList = new LinkedListNode<long>[itemCount];

for (var i = 0; i < itemCount; i++)
{
    nodeList[i] = organisingNode!;
    organisingNode = organisingNode.Next;
}

for (var i = 0; i < 10; i++)
foreach (var node in nodeList)
{
    var value = node.Value % (itemCount - 1);
    var nextNode = node.GetNext(1);
    var previous = node.GetPrevious(1);
    nums.Remove(node);

    var insertAfterNode = value switch
    {
        > 0 => nextNode.GetNext(value - 1),
        < 0 => previous.GetPrevious(-value),
        0 => previous
    };

    if (node != insertAfterNode)
    {
        nums.AddAfter(insertAfterNode, node);
    }
}

var zeroNode = nums.Find(0);
var first = zeroNode!.GetNext(1000);
var second = zeroNode!.GetNext(2000);
var third = zeroNode!.GetNext(3000);

Console.WriteLine(first.Value + second.Value + third.Value);
