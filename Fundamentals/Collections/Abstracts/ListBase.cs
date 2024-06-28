// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public abstract class ListBase<T> : IEnumerable<T>
{
    protected List<T> Items = new();

    public int Count => Items.Count;

    public T First() => Items.First();

    public T? FirstOrDefault() => Items.FirstOrDefault();

    public T Last() => Items.Last();

    public T? LastOrDefault() => Items.LastOrDefault();

    public void ForEach(Action<T> action) =>
        Items.ForEach(i => action(i));

    public IEnumerator<T> GetEnumerator() =>
        Items.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}