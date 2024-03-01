// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using System.Data.SqlTypes;
using static SquidEyes.Fundamentals.SoftArgStatus;

namespace SquidEyes.Fundamentals;

public static class SoftArgExtenders
{
    //public static SoftValue<T> Validate<T>(
    //    this SoftValue<T> value, ValidationResult result)
    //{
    //    if (!value.IsValid)
    //        result.Errors.Add(value.ToValidationFailure());

    //    return value;
    //}

    //////////////////////////

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

    //////////////////////////

    public static SoftValue<T> ToSoftValue<T>(this string input, 
        Tag tag, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new SoftValue<T>(tag, null!);
            else
                return new SoftValue<T>(tag, input!, NullOrEmpty);
        }

        if (!T.TryParse(input, null, out T value))
            return new SoftValue<T>(tag, input, ParseError);

        if (isValid is not null && !isValid(value))
            return new SoftValue<T>(tag, input, NotValid);

        return new SoftValue<T>(tag, value);
    }

    //////////////////////////

    public static SoftString ToSoftString(this string input, Tag tag,
        bool isOptional = false, Func<string, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new SoftString(tag, null!);
            else
                return new SoftString(tag, input!, NullOrEmpty);
        }

        if (isValid is not null && !isValid(input))
            return new SoftString(tag, input, NotValid);

        return new SoftString(tag, input);
    }

    //////////////////////////

    public static SoftUri ToSoftUri(this string input, Tag tag,
        UriKind uriKind = UriKind.Absolute,
        bool isOptional = false, Func<Uri, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new SoftUri(tag, null!);
            else
                return new SoftUri(tag, input!, NullOrEmpty);
        }

        if (!Uri.TryCreate(input, uriKind, out Uri? uri))
            return new SoftUri(tag, input, ParseError);

        if (isValid is not null && !isValid(uri))
            return new SoftUri(tag, input, NotValid);

        return new SoftUri(tag, uri);
    }

    //////////////////////////

    public static SoftEnum<T> ToSoftEnum<T>(this string input,
        Tag tag, bool isOptional = true, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new SoftEnum<T>(tag, null!);
            else
                return new SoftEnum<T>(tag, input!, NullOrEmpty);
        }

        if (!Enum.TryParse<T>(input, out var value))
            return new SoftEnum<T>(tag, input, ParseError);

        if (isValid is not null && !isValid(value))
            return new SoftEnum<T>(tag, input, NotValid);

        return new SoftEnum<T>(tag, value);
    }

    //////////////////////////

    public static bool TryGetErrors(string code,
        IEnumerable<ISoftArg> values, out List<Error> errors)
    {
        errors = [];

        foreach (var value in values)
        {
            if (!value.IsValid)
                errors.Add(value.ToError(code));
        }

        return errors.Count > 0;
    }
}