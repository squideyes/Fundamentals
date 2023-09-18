// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;

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

    public static bool IsBadInput(this string value) =>
        string.IsNullOrWhiteSpace(value) 
            || value.Any(c => !char.IsAsciiLetterOrDigit(c));
}