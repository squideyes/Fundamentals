// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Loyc.Geometry;
using System.Collections;

namespace SquidEyes.Fundamentals;

public class TagValueSet : IEquatable<TagValueSet>, IEnumerable<TagValue>
{
    private readonly Dictionary<Tag, TagValue> dict = [];

    public bool IsEmpty => dict.Count == 0;

    public void Add(Tag tag, int value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, double value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, bool value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, long value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, Tag value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, MultiTag value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, Email value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, Phone value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, DateTime value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, DateOnly value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, TimeSpan value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, TimeOnly value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(Tag tag, Uri value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add<T>(Tag tag, T value)
        where T : struct, Enum
    {
        dict.Add(tag, TagValue.Create(tag, value));
    }

    public void Add(Tag tag, string value) =>
        dict.Add(tag, TagValue.Create(tag, value));

    public void Add(TagValue tagValue) =>
        dict.Add(tagValue.Tag, tagValue);

    public void AddRange(IEnumerable<TagValue> tagValues)
    {
        foreach (var tagValue in tagValues)
            Add(tagValue);
    }

    public void Upsert(Tag tag, int value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, double value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, bool value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, long value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, Tag value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, MultiTag value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, Email value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, Phone value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, DateTime value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, DateOnly value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, TimeSpan value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, TimeOnly value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(Tag tag, Uri value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert<T>(Tag tag, T value)
        where T : struct, Enum
    {
        dict[tag] = TagValue.Create(tag, value);
    }

    public void Upsert(Tag tag, string value) =>
        dict[tag] = TagValue.Create(tag, value);

    public void Upsert(TagValue tagValue) =>
        dict[tagValue.Tag] = tagValue;

    public void UpsertRange(IEnumerable<TagValue> tagValues)
    {
        foreach (var tagValue in tagValues)
            Upsert(tagValue);
    }

    public T GetAs<T>(Tag tag) =>
        (T)Convert.ChangeType(dict[tag], typeof(T));

    public bool TryGetAs<T>(Tag tag, T value) =>
        Safe.TryGetValue(() => GetAs<T>(tag), out value);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<TagValue> GetEnumerator() =>
        dict.Values.GetEnumerator();

    public bool Equals(TagValueSet? other)
    {
        if (other is null)
            return false;

        return dict.Equals(other.dict);
    }

    public override bool Equals(object? other) =>
        other is TagValueSet tvs && Equals(tvs);

    public override int GetHashCode() => dict.GetHashCode();

    public static implicit operator TagValueSet(TagValue[] tagValues)
    {
        tagValues.MustBe().True(v => v.HasItems());

        var tvs = new TagValueSet();

        tagValues.ForEach(tvs.Add);

        return tvs;
    }
}