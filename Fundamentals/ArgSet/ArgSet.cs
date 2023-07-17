namespace SquidEyes.Fundamentals;

public class ArgSet : IEquatable<ArgSet>
{
    public int Count => Args.Count;

    public bool IsEmpty => Count == 0;

    internal Dictionary<MultiTag, Arg> Args { get; } = new();

    public bool ContainsKey(MultiTag key) => Args.ContainsKey(key);

    public void Set(MultiTag key, bool value) =>
        PrivateSet(key, value, ArgKind.Boolean);

    public void Set(MultiTag key, AccountId value) =>
        PrivateSet(key, value, ArgKind.AccountId);

    public void Set(MultiTag key, ActorId value) =>
        PrivateSet(key, value, ArgKind.ActorId);

    public void Set(MultiTag key, DateOnly value) =>
        PrivateSet(key, value, ArgKind.DateOnly);

    public void Set(MultiTag key, DateTime value) =>
        PrivateSet(key, value, ArgKind.DateTime);

    public void Set(MultiTag key, Delta value) =>
        PrivateSet(key, value, ArgKind.Delta);

    public void Set(MultiTag key, double value) =>
        PrivateSet(key, value, ArgKind.Double);

    public void Set(MultiTag key, Email value) =>
        PrivateSet(key, value, ArgKind.Email);

    public void Set<T>(MultiTag key, T value)
        where T : struct, Enum
    {
        PrivateSet(key, value, ArgKind.Enum);
    }

    public void Set(MultiTag key, float value) =>
        PrivateSet(key, value, ArgKind.Float);

    public void Set(MultiTag key, Guid value) =>
        PrivateSet(key, value, ArgKind.Guid);

    public void Set(MultiTag key, int value) =>
        PrivateSet(key, value, ArgKind.Int32);

    public void Set(MultiTag key, long value) =>
        PrivateSet(key, value, ArgKind.Int64);

    public void Set(MultiTag key, MultiTag value) =>
        PrivateSet(key, value, ArgKind.MultiTag);

    public void Set(MultiTag key, Phone value) =>
        PrivateSet(key, value, ArgKind.Phone);

    public void Set(MultiTag key, string value) =>
        PrivateSet(key, value, ArgKind.String);

    public void Set(MultiTag key, Tag value) =>
        PrivateSet(key, value, ArgKind.Tag);

    public void Set(MultiTag key, TimeOnly value) =>
        PrivateSet(key, value, ArgKind.TimeOnly);

    public void Set(MultiTag key, TimeSpan value) =>
        PrivateSet(key, value, ArgKind.TimeSpan);

    public void Set(MultiTag key, TradeDate value) =>
        PrivateSet(key, value, ArgKind.TradeDate);

    public void Set(MultiTag key, Uri value) =>
        PrivateSet(key, value, ArgKind.Uri);

    internal void Set(MultiTag key, Enum value) =>
        PrivateSet(key, value, ArgKind.Enum);

    internal void Set(MultiTag key, Arg arg) => Args[key] = arg;

    public T Get<T>(MultiTag key) => (T)Args[key].Value;

    public T Get<T>(MultiTag key, T @default)
    {
        if (@default.IsDefault())
            throw new ArgumentOutOfRangeException(nameof(@default));

        if (Args.TryGetValue(key, out Arg? arg))
        {
            if (arg.Value.TryCast(out T result))
                return result;

            var article = arg.Kind.ToString()[0].IsPlural() ? "An" : "A";

            throw new InvalidCastException(
                $"{article} {arg.Kind} argument may not be cast to a \"{typeof(T)}\".");
        }

        return @default;
    }

    private void PrivateSet(MultiTag key, object value, ArgKind kind)
    {
        key.MayNotBe().Null();
        value.MayNotBe().Default();

        Args[key] = Arg.Create(value!, kind);
    }

    public bool Equals(ArgSet? other)
    {
        if (other is null)
            return false;

        if (other.Count != Count)
            return false;

        if (!Args.Keys.All(other.ContainsKey))
            return false;

        return Args.Keys.All(k => other.Args[k] == Args[k]);
    }

    public override bool Equals(object? other) =>
        other is ArgSet argSet && Equals(argSet);

    public override int GetHashCode() => Args.GetHashCode();

    public static bool operator ==(ArgSet left, ArgSet right)
    {
        if (left is null)
            return right is null;

        return left.Equals(right);
    }

    public static bool operator !=(ArgSet left, ArgSet right) =>
        !(left == right);
}