// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class LookupSetBase<T>
{
    private readonly Dictionary<Identifier, T> dict = new();

    public void Add(Identifier name, T value)
    {
        name.MayNot().BeDefault();

        ValidateValue(value);

        dict.Add(name, value);
    }

    public T this[Identifier name] => dict[name];

    public bool TryGetValue(Identifier name, out T value) =>
        dict.TryGetValue(name, out value!);

    protected abstract void ValidateValue(T value);
}