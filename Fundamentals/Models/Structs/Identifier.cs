// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public readonly struct Identifier : IEquatable<Identifier>, IComparable<Identifier>
{
    private Identifier(string value)
    {
        Value = value;
    }

    private string Value { get; }

    public static Identifier From(string value)
    {
        if (!value.IsTokenValue())
            throw new ArgumentOutOfRangeException(nameof(value));

        return new Identifier(value);
    }

    public int CompareTo(Identifier other) => Value.CompareTo(other.Value);

    public bool Equals(Identifier other) => Value.Equals(other.Value);

    public override string ToString() => Value;

    public override bool Equals(object? other) =>
        other is Identifier identifier && Equals(identifier);

    public override int GetHashCode() => Value.GetHashCode();

    public static bool operator ==(Identifier left, Identifier right) =>
        left.Equals(right);

    public static bool operator !=(Identifier left, Identifier right) =>
        !(left == right);

    public static bool operator <(Identifier left, Identifier right) =>
        left.CompareTo(right) < 0;

    public static bool operator <=(Identifier left, Identifier right) =>
        left.CompareTo(right) <= 0;

    public static bool operator >(Identifier left, Identifier right) =>
        left.CompareTo(right) > 0;

    public static bool operator >=(Identifier left, Identifier right) =>
        left.CompareTo(right) >= 0;

    public static implicit operator Identifier(string value) => From(value);

    public static implicit operator string(Identifier value) => value.Value;
}