// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public readonly struct SeriesData<T>
{
    internal SeriesData(T value, bool wasSet)
    {
        Value = value;
        WasSet = wasSet;
    }

    public T Value { get; }
    public bool WasSet { get; }

    public override string ToString() =>
        Value + "; " + (WasSet ? "SET" : "NOT SET");
}