namespace Day13;

public static class ParseUtils
{
    public static Queue<char> ParsePacketToList(string packet)
    {
        var queue = new Queue<char>();
        
        foreach (var p in packet)
        {
            queue.Enqueue(p);
        }

        return queue;
    }

    public static List<object> HandleList(Queue<char> queue)
    {
        List<object> elements = new ();
        queue.Dequeue();

        while (queue.Peek() != ']')
        {
            if (queue.Peek() == ',') queue.Dequeue();
            
            elements.Add(HandleElement(queue));
        }

        queue.Dequeue();
        return elements;
    }

    private static object HandleElement(Queue<char> queue)
    {
        var c = queue.Peek();
        
        if (char.IsDigit(c))
        {
            var number = "";
            
            while (char.IsDigit(queue.Peek()))
            {
                number += queue.Dequeue();
            }

            return int.Parse($"{number}");
        }

        if (c == '[')
        {
            return HandleList(queue);
        }

        throw new Exception("Something is wrong.");
    }
}