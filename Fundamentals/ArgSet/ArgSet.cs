// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections;

namespace SquidEyes.Fundamentals;

public class ArgSet : IEnumerable<KeyValuePair<ConfigKey, Arg>>
{
    private readonly Dictionary<ConfigKey, Arg> args = new();

    public int Count => args.Count;

    public bool IsEmpty => Count == 0;

    public void Upsert(ConfigKey key, AccountId value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, bool value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, ClientId value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, DateOnly value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, DateTime value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, double value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Email value) =>
        SimpleUpsert(key, value);

    public void Upsert<T>(ConfigKey key, T value)
        where T : Enum
    {
        SimpleUpsert(key, value);
    }

    internal void UpsertEnum(ConfigKey key, object value)
    {
        if (!value.GetType().IsEnum)
            throw new ArgumentOutOfRangeException(nameof(value));

        SimpleUpsert(key, value);
    }

    public void Upsert(ConfigKey key, float value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Guid value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, int value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, long value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Offset value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Phone value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Ratchet value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, ShortId value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, string value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, TimeOnly value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, TimeSpan value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Token value) =>
        SimpleUpsert(key, value);

    private void SimpleUpsert<T>(ConfigKey key, T value)
    {
        key.MayNot().BeDefault();

        args[key] = new Arg(value!);
    }

    public AccountId GetAccountId(ConfigKey key) => (AccountId)args[key].Value;

    public bool GetBoolean(ConfigKey key) => (bool)args[key].Value;

    public ClientId GetClientId(ConfigKey key) => (ClientId)args[key].Value;

    public DateOnly GetDateOnly(ConfigKey key) => (DateOnly)args[key].Value;

    public DateTime GetDateTime(ConfigKey key) => (DateTime)args[key].Value;

    public double GetDouble(ConfigKey key) => (double)args[key].Value;

    public Email GetEmail(ConfigKey key) => (Email)args[key].Value;

    public T GetEnum<T>(ConfigKey key)
    {
        if (!typeof(T).IsEnum)
            throw new ArgumentOutOfRangeException(nameof(T));

        return (T)args[key].Value;
    }

    public Guid GetGuid(ConfigKey key) => (Guid)args[key].Value;

    public float GetFloat(ConfigKey key) => (float)args[key].Value;

    public int GetInt32(ConfigKey key) => (int)args[key].Value;

    public long GetInt64(ConfigKey key) => (long)args[key].Value;

    public Offset GetOffset(ConfigKey key) => (Offset)args[key].Value;

    public Phone GetPhone(ConfigKey key) => (Phone)args[key].Value;

    public Ratchet GetRatchet(ConfigKey key) => (Ratchet)args[key].Value;

    public ShortId GetShortId(ConfigKey key) => (ShortId)args[key].Value;

    public string GetString(ConfigKey key) => (string)args[key].Value;

    public TimeOnly GetTimeOnly(ConfigKey key) => (TimeOnly)args[key].Value;

    public TimeSpan GetTimeSpan(ConfigKey key) => (TimeSpan)args[key].Value;

    public Token GetToken(ConfigKey key) => (Token)args[key].Value;

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerator<KeyValuePair<ConfigKey, Arg>> GetEnumerator() =>
        args.GetEnumerator();
}