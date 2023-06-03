// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class ArgSet : IEnumerable<KeyValuePair<MultiTag, Arg>>
{
    private readonly Dictionary<MultiTag, Arg> args = new();

    public int Count => args.Count;

    public bool IsEmpty => Count == 0;

    public void Upsert(MultiTag multiTag, AccountId value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, bool value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, ClientId value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, DateOnly value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, DateTime value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, double value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Email value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert<T>(MultiTag multiTag, T value)
        where T : Enum
    {
        SimpleUpsert(multiTag, value);
    }

    internal void UpsertEnum(MultiTag multiTag, object value)
    {
        if (!value.GetType().IsEnum)
            throw new ArgumentOutOfRangeException(nameof(value));

        SimpleUpsert(multiTag, value);
    }

    public void Upsert(MultiTag multiTag, float value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Guid value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, int value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, long value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Offset value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Phone value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Ratchet value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, ShortId value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, string value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, TimeOnly value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, TimeSpan value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Tag value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Uri value)
    {
        value.IsAbsoluteUri.Must().Be(true);

        SimpleUpsert(multiTag, value);
    }

    private void SimpleUpsert<T>(MultiTag multiTag, T value)
    {
        multiTag.MayNot().BeDefault();

        args[multiTag] = new Arg(value!);
    }

    public AccountId GetAccountId(MultiTag multiTag) => (AccountId)args[multiTag].Value;

    public bool GetBoolean(MultiTag multiTag) => (bool)args[multiTag].Value;

    public ClientId GetClientId(MultiTag multiTag) => (ClientId)args[multiTag].Value;

    public DateOnly GetDateOnly(MultiTag multiTag) => (DateOnly)args[multiTag].Value;

    public DateTime GetDateTime(MultiTag multiTag) => (DateTime)args[multiTag].Value;

    public double GetDouble(MultiTag multiTag) => (double)args[multiTag].Value;

    public Email GetEmail(MultiTag multiTag) => (Email)args[multiTag].Value;

    public T GetEnum<T>(MultiTag multiTag)
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentOutOfRangeException(nameof(T));

        return (T)args[multiTag].Value;
    }

    public Guid GetGuid(MultiTag multiTag) => (Guid)args[multiTag].Value;

    public float GetFloat(MultiTag multiTag) => (float)args[multiTag].Value;

    public int GetInt32(MultiTag multiTag) => (int)args[multiTag].Value;

    public long GetInt64(MultiTag multiTag) => (long)args[multiTag].Value;

    public Offset GetOffset(MultiTag multiTag) => (Offset)args[multiTag].Value;

    public Phone GetPhone(MultiTag multiTag) => (Phone)args[multiTag].Value;

    public Ratchet GetRatchet(MultiTag multiTag) => (Ratchet)args[multiTag].Value;

    public ShortId GetShortId(MultiTag multiTag) => (ShortId)args[multiTag].Value;

    public string GetString(MultiTag multiTag) => (string)args[multiTag].Value;

    public TimeOnly GetTimeOnly(MultiTag multiTag) => (TimeOnly)args[multiTag].Value;

    public TimeSpan GetTimeSpan(MultiTag multiTag) => (TimeSpan)args[multiTag].Value;

    public Tag GetTag(MultiTag multiTag) => (Tag)args[multiTag].Value;

    public Uri GetUri(MultiTag multiTag) => (Uri)args[multiTag].Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<MultiTag, Arg>> GetEnumerator() =>
        args.GetEnumerator();
}