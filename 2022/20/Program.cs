using _20;

const int key = 811589153;
var nums = new LinkedList<int>((await File.ReadAllLinesAsync("Input.txt")).Select(int.Parse));

var itemCount = nums.Count;
var visitedNodes = new HashSet<LinkedListNode<int>>();

var node = nums.First;

while (true)
{
    if (node is null) break;
    var next = node.Next;

    if (!visitedNodes.Contains(node))
    {
        visitedNodes.Add(node);
        
        var value = node.Value % (itemCount - 1);
        var nextNode = node.GetNext(1);
        var previous = node.GetPrevious(1);
        nums.Remove(node);

        var insertAfterNode = value switch
        {
            > 0 => nextNode.GetNext(value - 1),
            < 0 => previous.GetPrevious(- value),
            0 => previous
        };

        if (node != insertAfterNode)
        {
            nums.AddAfter(insertAfterNode, node);
        }
    }

    node = next;
}

var zeroNode = nums.Find(0);
var first = zeroNode!.GetNext(1000);
var second = zeroNode!.GetNext(2000);
var third = zeroNode!.GetNext(3000);

Console.WriteLine(first.Value + second.Value + third.Value);
