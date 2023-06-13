namespace _13;

public static class Functions
{
    public static UnparsedPair ToPairs(string[] rawInput) =>
        new(rawInput[0], rawInput[1]);
    
    public static bool IsProperlyOrdered(UnparsedPair pair) =>
        AreElementsOrdered(CreateElement(pair.First), CreateElement(pair.Second)) ??
        throw new Exception("Elements are equal");

    public static Element CreateElement(this ReadOnlySpan<char> input)
    {
        var elementList = new List<Element>();
        var i = 0;
        while (true)
        {
            if (i >= input.Length) break;
            
            if (input[i] is '[')
            {
                var elementEnd = FindElementEndIndex(input[(i + 1)..]);
                var element = input.Slice(i + 1, elementEnd);
                i += elementEnd + 3; // skip over trailing ],
                elementList.Add(CreateElement(element));
                continue;
            }
            
            if (char.IsDigit(input[i]))
            {
                var numString = GetNumberString(input[i..]);
                i += numString.Length + 1;
                elementList.Add(new DigitElement(int.Parse(numString)));
                continue;
            }

            throw new Exception($"Unexpected character {input[i]}");
        }

        return elementList.Count is 1 ? elementList[0] : new ArrayElement(elementList);
    }

    public static bool? AreElementsOrdered(Element first, Element second)
    {
        if (first is DigitElement firstDigit && second is DigitElement secondDigit)
        {
            return (secondDigit.Value - firstDigit.Value) switch
            {
                > 0 => true,
                < 0 => false,
                _ => null
            };
        }

        if (first is ArrayElement firstArray && second is ArrayElement secondArray)
        {
            var i = 0;
            var left = firstArray.Values;
            var right = secondArray.Values;
            while (true)
            {
                if (i == left.Count && i == right.Count) return null;
                if (i == left.Count) return true;
                if (i == right.Count) return false;

                var elementCompare = AreElementsOrdered(left[i], right[i]);
                if (elementCompare.HasValue) return elementCompare.Value;

                i++;
            }
        }

        if (first is DigitElement fd) return AreElementsOrdered(new ArrayElement(new[] { fd }), second);
        if (second is DigitElement sd) return AreElementsOrdered(first, new ArrayElement(new []{sd}));

        throw new Exception();
    }

    private static string GetNumberString(this ReadOnlySpan<char> input)
    {
        IEnumerable<char> num = Enumerable.Empty<char>();

        foreach (var character in input)
        {
            if (!char.IsDigit(character)) break;

            num = num.Append(character);
        }
        
        return new string(num.ToArray());
    }

    private static int FindElementEndIndex(ReadOnlySpan<char> input)
    {
        var count = 1;
        for (var i = 0; i < input.Length; i++)
        {
            switch (input[i])
            {
                case '[':
                    count++;
                    break;
                case ']':
                    count--;
                    break;
            }

            if (count is 0) return i;
        }

        throw new Exception();
    }
}