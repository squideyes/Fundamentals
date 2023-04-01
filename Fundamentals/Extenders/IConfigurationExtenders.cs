// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Configuration;

namespace SquidEyes.Fundamentals;

public static class IConfigurationExtenders
{
    public static T GetAs<T>(this IConfiguration config, ConfigKey key)
        where T : IConvertible
    {
        config.MayNot().BeNull();
        key.MayNot().BeDefault();

        return (T)Convert.ChangeType(
            config[key.ToString()]!, typeof(T));
    }
}