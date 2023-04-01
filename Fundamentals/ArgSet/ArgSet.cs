// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ArgSet
{
    private readonly Dictionary<ConfigKey, Arg> args = new();

    public bool IsEmpty => args.Count == 0;

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

    public void Upsert(ConfigKey key, float value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Guid value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, int value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, long value) =>
        SimpleUpsert(key, value);

    public void Upsert(ConfigKey key, Phone value) =>
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

    public T Get<T>(ConfigKey key)
    {
        key.MayNot().BeDefault();

        return (T)Convert.ChangeType(args[key], typeof(T));
    }
}