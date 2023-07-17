// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public abstract class LookupSetBase<T>
{
    private readonly Dictionary<Tag, T> dict = new();

    public void Add(Tag tag, T value)
    {
        tag.MayNotBe().Default();

        ValidateValue(value);

        dict.Add(tag, value);
    }

    public T this[Tag tag] => dict[tag];

    public bool TryGetValue(Tag tag, out T value) =>
        dict.TryGetValue(tag, out value!);

    protected abstract void ValidateValue(T value);
}