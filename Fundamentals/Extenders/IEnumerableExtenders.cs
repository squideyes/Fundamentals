// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class IEnumerableExtenders
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        foreach (var item in items)
            action(item);
    }

    public static bool IsUnique<T>(this IEnumerable<T> values) =>
        values.All(new HashSet<T>().Add);

    public static bool HasItems<T>(
        this IEnumerable<T> items, bool nonDefault = true)
    {
        return items.HasItems(1, int.MaxValue,
            v => !nonDefault || !Equals(v, default(T)));
    }

    public static bool HasItems<T>(
        this IEnumerable<T> items, Func<T, bool>? isValid)
    {
        return items.HasItems(1, int.MaxValue, isValid);
    }

    public static bool HasItems<T>(this IEnumerable<T> items,
        int minItems, int maxItems, bool nonDefault = true)
    {
        return items.HasItems(minItems, maxItems, 
            v => !nonDefault || !Equals(v, default(T)));
    }

    public static bool HasItems<T>(this IEnumerable<T> items,
        int minItems, int maxItems, Func<T, bool>? isValid)
    {
        if (minItems < 1)
            throw new ArgumentOutOfRangeException(nameof(minItems));

        if (maxItems < minItems)
            throw new ArgumentOutOfRangeException(nameof(maxItems));

        int count = 0;

        foreach (var item in items)
        {
            if (isValid != null && !isValid(item))
                return false;

            if (++count > maxItems)
                return false;
        }

        return count >= minItems;
    }

    public static IEnumerable<List<T>> Chunked<T>(
        this IEnumerable<T> enumerable, int chunkSize)
    {
        static IEnumerable<T> GetChunk(IEnumerator<T> e, 
            Func<bool> innerMoveNext)
        {
            do
            {
                yield return e.Current;
            }
            while (innerMoveNext());
        }

        if (chunkSize < 1)
            throw new ArgumentOutOfRangeException(nameof(chunkSize));

        using var e = enumerable.GetEnumerator();

        while (e.MoveNext())
        {
            var remaining = chunkSize;

            var innerMoveNext = new Func<bool>(
                () => --remaining > 0 && e.MoveNext());

            yield return GetChunk(e, innerMoveNext).ToList();

            while (innerMoveNext()) ;
        }
    }
}