// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Configuration;

namespace SquidEyes.Fundamentals;

public static class IConfigurationExtenders
{
    public static T GetAs<T>(this IConfiguration config, MultiTag key)
        where T : IConvertible
    {
        config.MayNot().BeNull();
        key.MayNot().BeDefault();

        return (T)Convert.ChangeType(config[key.ToString()]!, typeof(T));
    }

    public static T? GetValueOrDefault<T>(this IConfiguration config,
       string key, Func<string, bool> isValid, Func<string, T> getValue)
    {
        config.MayNot().BeNull();
        key.MayNot().BeDefault();
        isValid.MayNot().BeNull();
        getValue.MayNot().BeNull();

        var value = config[key];

        if (value == null)
            return default;

        if (isValid(value))
            return getValue(value);
        else
            return default;
    }
}