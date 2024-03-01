// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal class Arg : IEquatable<Arg>
{
    private Arg(object value, ArgKind kind)
    {
        Kind = kind;
        Value = value;

        if (kind == ArgKind.Enum)
            Type = value.GetType();
        else
            Type = null!;
    }

    internal ArgKind Kind { get; }
    internal object Value { get; }
    internal Type Type { get; }

    public bool Equals(Arg? other)
    {
        return other is not null
            && other.Kind == Kind
            && other.Value == Value
            && other.Type == Type;
    }

    public override bool Equals(object? other) =>
        other is Arg arg && Equals(arg);

    public override int GetHashCode() => HashCode.Combine(Kind, Value, Type);

    internal string GetFormattedValue()
    {
        return Kind switch
        {
            ArgKind.DateOnly => Value.As<DateOnly>().ToString("MM/dd/yyyy"),
            ArgKind.DateTime => Value.As<DateTime>().ToString("MM/dd/yyyy HH:mm:ss.fff"),
            ArgKind.TimeSpan => Value.As<TimeSpan>().ToString(@"d\.hh\:mm\:ss\.fff"),
            ArgKind.TimeOnly => Value.As<TimeOnly>().ToString("HH:mm:ss.fff"),
            _ => Value.ToString()!
        };
    }

    internal static Arg Create<T>(T value, ArgKind kind) => new(value!, kind);

    public static bool operator ==(Arg left, Arg right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(Arg left, Arg right) =>
        !(left == right);
}