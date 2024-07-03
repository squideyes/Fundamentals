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

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<ITagArg> GetEnumerator() => dict.Values.GetEnumerator();
}
