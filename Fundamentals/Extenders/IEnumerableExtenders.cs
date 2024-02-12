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

        chunkSize.MustBe().Positive();

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