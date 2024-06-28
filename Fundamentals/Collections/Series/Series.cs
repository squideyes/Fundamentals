// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class Series<T> : IEnumerable<SeriesData<T>>
{
    private readonly List<SeriesData<T>> datas = new();

    public void Add(T value) =>
        datas.Insert(0, new SeriesData<T>(value, true));

    public void Add() => datas.Insert(0, new SeriesData<T>());

    public int Count => datas.Count;

    public T this[int index]
    {
        get => datas[index].Value;
        set => datas[index] = new SeriesData<T>(value, true);
    }

    public bool GetWasSet(int index) => datas[index].WasSet;

    public void Reset(int index = 0) =>
        datas[index] = new SeriesData<T>(datas[index].Value, false);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<SeriesData<T>> GetEnumerator() => datas.GetEnumerator();
}