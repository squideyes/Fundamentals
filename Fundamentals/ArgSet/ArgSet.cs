// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class ArgSet : IEnumerable<KeyValuePair<MultiTag, Arg>>
{
    private readonly Dictionary<MultiTag, Arg> Args = new();

    public int Count => Args.Count;

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

    public void Upsert(MultiTag multiTag, Delta value) =>
        SimpleUpsert(multiTag, value);

    public void Upsert(MultiTag multiTag, Phone value) =>
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

        Args[multiTag] = new Arg(value!);
    }

    public AccountId GetAccountId(MultiTag multiTag) => (AccountId)Args[multiTag].Value;

    public bool GetBoolean(MultiTag multiTag) => (bool)Args[multiTag].Value;

    public ClientId GetClientId(MultiTag multiTag) => (ClientId)Args[multiTag].Value;

    public DateOnly GetDateOnly(MultiTag multiTag) => (DateOnly)Args[multiTag].Value;

    public DateTime GetDateTime(MultiTag multiTag) => (DateTime)Args[multiTag].Value;

    public double GetDouble(MultiTag multiTag) => (double)Args[multiTag].Value;

    public Email GetEmail(MultiTag multiTag) => (Email)Args[multiTag].Value;

    public T GetEnum<T>(MultiTag multiTag)
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentOutOfRangeException(nameof(T));

        return (T)Args[multiTag].Value;
    }

    public Guid GetGuid(MultiTag multiTag) => (Guid)Args[multiTag].Value;

    public float GetFloat(MultiTag multiTag) => (float)Args[multiTag].Value;

    public int GetInt32(MultiTag multiTag) => (int)Args[multiTag].Value;

    public long GetInt64(MultiTag multiTag) => (long)Args[multiTag].Value;

    public Delta GetDelta(MultiTag multiTag) => (Delta)Args[multiTag].Value;

    public Phone GetPhone(MultiTag multiTag) => (Phone)Args[multiTag].Value;

    public string GetString(MultiTag multiTag) => (string)Args[multiTag].Value;

    public TimeOnly GetTimeOnly(MultiTag multiTag) => (TimeOnly)Args[multiTag].Value;

    public TimeSpan GetTimeSpan(MultiTag multiTag) => (TimeSpan)Args[multiTag].Value;

    public Tag GetTag(MultiTag multiTag) => (Tag)Args[multiTag].Value;

    public Uri GetUri(MultiTag multiTag) => (Uri)Args[multiTag].Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<MultiTag, Arg>> GetEnumerator() =>
        Args.GetEnumerator();
}