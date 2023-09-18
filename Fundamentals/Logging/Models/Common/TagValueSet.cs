// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class TagValueSet : IEnumerable<KeyValuePair<Tag, object>>
{

    private readonly Dictionary<Tag, object> dict = new();

    public bool IsEmpty => !dict.Any();

    public void Add(Tag tag, object value) =>
        dict.Add(tag.MayNotBe().Default(), value);

    public T Get<T>(Tag tag) => 
        (T)Convert.ChangeType(dict[tag], typeof(T));

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<Tag, object>> GetEnumerator() =>
        dict.GetEnumerator();

    public static TagValueSet From(string format, params object[] args)
    {
        return new TagValueSet()
        {
            { Tag.Create("Message"), string.Format(format, args) }
        };
    }
}