// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using static SquidEyes.Fundamentals.ConfigValueStatus;

namespace SquidEyes.Fundamentals;

public static class ConfigHelper
{
    public static ConfigValue<int> CreateInt32(
        Tag tag, string input, Func<int, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<int> result))
            return result;
        else if (!int.TryParse(input, out int value))
            return new ConfigValue<int>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<int>(tag, input, NotValid);
        else
            return new ConfigValue<int>(tag, value);
    }

    public static ConfigValue<long> CreateInt64(
        Tag tag, string input, Func<long, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<long> result))
            return result;
        else if (!long.TryParse(input, out long value))
            return new ConfigValue<long>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<long>(tag, input, NotValid);
        else
            return new ConfigValue<long>(tag, value);
    }

    public static ConfigValue<double> CreateDouble(
        Tag tag, string input, Func<double, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<double> result))
            return result;
        else if (!double.TryParse(input, out double value))
            return new ConfigValue<double>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<double>(tag, input, NotValid);
        else
            return new ConfigValue<double>(tag, value);
    }

    public static ConfigValue<float> CreateFloat(
        Tag tag, string input, Func<float, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<float> result))
            return result;
        else if (!float.TryParse(input, out float value))
            return new ConfigValue<float>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<float>(tag, input, NotValid);
        else
            return new ConfigValue<float>(tag, value);
    }

    public static ConfigValue<DateTime> CreateDateTime(
        Tag tag, string input, Func<DateTime, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<DateTime> result))
            return result;
        else if (!DateTime.TryParse(input, out DateTime value))
            return new ConfigValue<DateTime>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<DateTime>(tag, input, NotValid);
        else
            return new ConfigValue<DateTime>(tag, value);
    }

    public static ConfigValue<DateOnly> CreateDateOnly(
        Tag tag, string input, Func<DateOnly, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<DateOnly> result))
            return result;
        else if (!DateOnly.TryParse(input, out DateOnly value))
            return new ConfigValue<DateOnly>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<DateOnly>(tag, input, NotValid);
        else
            return new ConfigValue<DateOnly>(tag, value);
    }

    public static ConfigValue<TimeSpan> CreateTimeSpan(
        Tag tag, string input, Func<TimeSpan, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<TimeSpan> result))
            return result;
        else if (!TimeSpan.TryParse(input, out TimeSpan value))
            return new ConfigValue<TimeSpan>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<TimeSpan>(tag, input, NotValid);
        else
            return new ConfigValue<TimeSpan>(tag, value);
    }

    public static ConfigValue<TimeOnly> CreateTimeOnly(
        Tag tag, string input, Func<TimeOnly, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<TimeOnly> result))
            return result;
        else if (!TimeOnly.TryParse(input, out TimeOnly value))
            return new ConfigValue<TimeOnly>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<TimeOnly>(tag, input, NotValid);
        else
            return new ConfigValue<TimeOnly>(tag, value);
    }

    public static ConfigValue<bool> CreateBool(
        Tag tag, string input, Func<bool, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<bool> result))
            return result;
        else if (!bool.TryParse(input, out bool value))
            return new ConfigValue<bool>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<bool>(tag, input, NotValid);
        else
            return new ConfigValue<bool>(tag, value);
    }

    public static ConfigValue<Guid> CreateGuid(
        Tag tag, string input, Func<Guid, bool> isValid = null!)
    {
        if (!HasBasics(tag, input, out ConfigValue<Guid> result))
            return result;
        else if (!Guid.TryParse(input, out Guid value))
            return new ConfigValue<Guid>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<Guid>(tag, input, NotValid);
        else
            return new ConfigValue<Guid>(tag, value);
    }

    public static ConfigValue<T> CreateEnum<T>(
        Tag tag, string input, Func<T, bool> isValid = null!)
        where T : struct, Enum
    {
        if (!HasBasics(tag, input, out ConfigValue<T> result))
            return result;
        else if (!Enum.TryParse(input, out T value))
            return new ConfigValue<T>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<T>(tag, input, NotValid);
        else
            return new ConfigValue<T>(tag, value);
    }

    public static ConfigValue<string> CreateString(
        Tag tag, string input, Func<string, bool> isValid)
    {
        if (isValid is not null && !isValid(input))
            return new ConfigValue<string>(tag, input, NotValid);
        else
            return new ConfigValue<string>(tag, input);
    }

    public static ConfigValue<Uri> CreateUri(Tag tag, string input, 
        UriKind uriKind = UriKind.Absolute, Func<Uri, bool> isValid = null!)
    {
        if (input is null)
            return new ConfigValue<Uri>(tag, input!, NullInput);
        else if (!Uri.TryCreate(input, uriKind, out Uri? value))
            return new ConfigValue<Uri>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new ConfigValue<Uri>(tag, input, NotValid);
        else
            return new ConfigValue<Uri>(tag, value);
    }

    private static bool HasBasics<T>(
        Tag tag, string input, out ConfigValue<T> result)
    {
        result = null!;

        if (input is null)
            result = new ConfigValue<T>(tag, input!, NullInput);
        else if (input.IsBadInput())
            result = new ConfigValue<T>(tag, input, BadInput);

        return result == null;
    }

    public static bool TryGetErrors(string code,
        IEnumerable<IConfigValue> values, out List<Error> errors)
    {
        errors = [];

        foreach (var value in values)
        {
            if (!value.IsValid)
                errors.Add(value.ToError(code));
        }

        return errors.Count > 0;
    }

    public static Error GetValidationError(
        string code, Tag tag, string message)
    {
        code.MustBe().True(v => v is null || v.IsNonNullAndTrimmed());
        tag.MayNotBe().Default();
        message.MustBe().NonNullAndTrimmed();

        return Error.Validation(code, tag + ": " + message!);
    }

}