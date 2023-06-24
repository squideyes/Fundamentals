// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class ValueObjectBase<T>
    : IEquatable<ValueObjectBase<T>>, IComparable<ValueObjectBase<T>>
    where T : ValueObjectBase<T>, new()
{
    public string? Input { get; protected set; }

    public override bool Equals(object? other) =>
        other is ValueObjectBase<T> vo && Input!.Equals(vo.Input);

    public bool Equals(ValueObjectBase<T>? other) =>
        other is not null && Input!.Equals(other.Input);

    public override string ToString() => Input!;

    public override int GetHashCode() => Input!.GetHashCode();

    public int CompareTo(ValueObjectBase<T>? other) =>
        Input!.CompareTo(other?.Input);

    protected abstract void SetProperties(string input);

    protected static T DoCreate(string input, Func<string, bool> isInput)
    {
        if (!DoTryCreate(input, isInput, out T result))
        {
            throw new ArgumentOutOfRangeException(nameof(input),
                $"\"{input}\" is an invalid {typeof(T).Name} input.");
        }

        return result;
    }

    protected static bool DoTryCreate(
        string input, Func<string, bool> isInput, out T result)
    {
        result = null!;

        if (string.IsNullOrEmpty(input))
            return false;

        if (!isInput(input))
            return false;

        result = new T() { Input = input };

        result.SetProperties(input);

        return true;
    }

    public static bool operator ==(ValueObjectBase<T>? left, ValueObjectBase<T>? right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(ValueObjectBase<T>? left, ValueObjectBase<T>? right) =>
        !(left == right);

    private static int Compare(ValueObjectBase<T> left, ValueObjectBase<T> right)
    {
        if (left is null && right is null)
            return 0;
        else if (left is null)
            return -1;
        else if (right is null)
            return 1;
        else
            return left.Input!.CompareTo(right.Input);
    }

    public static bool operator <(ValueObjectBase<T>? left, ValueObjectBase<T>? right) =>
        Compare(left!, right!) < 0;

    public static bool operator <=(ValueObjectBase<T>? left, ValueObjectBase<T>? right) =>
        Compare(left!, right!) <= 0;

    public static bool operator >(ValueObjectBase<T>? left, ValueObjectBase<T>? right) =>
        Compare(left!, right!) > 0;

    public static bool operator >=(ValueObjectBase<T>? left, ValueObjectBase<T>? right) =>
        Compare(left!, right!) >= 0;
}