namespace SquidEyes.Fundamentals;

public abstract class LookupSetBase<T>
{
    private readonly Dictionary<Token, T> dict = new();

    public void Add(Token name, T value)
    {
        name.MayNot().BeDefault();

        ValidateValue(value);

        dict.Add(name, value);
    }

    public T this[Token name] => dict[name];

    public bool TryGetValue(Token name, out T value) =>
        dict.TryGetValue(name, out value!);

    protected abstract void ValidateValue(T value);
}