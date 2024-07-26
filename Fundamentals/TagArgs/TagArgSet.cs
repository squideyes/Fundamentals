// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class TagArgSet : IEnumerable<ITagArg>
{
    private readonly Dictionary<Tag, ITagArg> dict = [];

    public int Count => dict.Count;

    public ITagArg this[Tag tag]
    {
        get => dict[tag];
        set => dict[tag] = value;
    }

    public void Add(ITagArg tagArg) => dict.Add(tagArg.Tag, tagArg);

    public void AddRange(IEnumerable<ITagArg> tagArgs) => tagArgs.ForEach(Add);

    public Dictionary<string, object> ToSimplifiedDictionary() =>
        dict.Values.Select(a => new KeyValuePair<string, object>(
            a.Tag.Value!, a.GetArgAsObject())).ToDictionary();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<ITagArg> GetEnumerator() => dict.Values.GetEnumerator();
}