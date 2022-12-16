namespace Day13;

public static class Comparator
{
    public static bool? CompareLists(List<object> left, List<object> right)
    {
        var elemCount = Math.Min(left.Count, right.Count);

        for (int i = 0; i < elemCount; i++)
        {
            var isLeftSmaller = CompareElements(left[i], right[i]);

            if (!isLeftSmaller.HasValue) continue;
            
            return isLeftSmaller;
        }

        if (left.Count == right.Count) return null;

        return left.Count < right.Count;
    }

    public static bool? CompareElements(object left, object right)
    {
        return (left, right) switch
        {
            (int l, int r) => IntParser(l,r),
            (List<object> l, int r) => CompareLists(l, new List<object> { r }),
            (int l, List<object> r) => CompareLists(new List<object> { l }, r),
            (List<object> l, List<object> r) => CompareLists(l, r),
            _ => throw new Exception("Error.")
        };
    }

    private static bool? IntParser(int l, int r)
    {
        if (l == r) return null;

        return l < r;
    }
}