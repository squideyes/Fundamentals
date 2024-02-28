// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

namespace SquidEyes.Fundamentals;

public static class ConfigExtenders
{
    public static ConfigValue<T> Validate<T>(
        this ConfigValue<T> value, ValidationResult result)
    {
        if (!value.IsValid)
            result.Errors.Add(value.ToValidationFailure());

        return value;
    }

    public static ErrorOr<Dictionary<string, string>> GetDict(
        this IConfiguration config, string key,
        string errorOrCode, bool mayBeEmpty = false,
        Func<KeyValuePair<string, string>, bool> isValid = null!)
    {
        Dictionary<string, string> GetDict(string key)
        {
            var dict = new Dictionary<string, string>();

            config.GetSection(key).Bind(dict);

            return dict;
        }

        Error AddBadKeyValueError(string suffix)
        {
            return Error.Validation(errorOrCode,
                $"One or more \"{key}\" key/values {suffix}!");
        }

        var dict = GetDict(key);

        if (dict.HasItems())
        {
            if (isValid is null || dict.All(kv => isValid(kv)))
                return dict;
            else
                return AddBadKeyValueError("are invalid");
        }
        else
        {
            if (mayBeEmpty)
                return dict;
            else
                return AddBadKeyValueError("must be supplied!");
        }
    }
}