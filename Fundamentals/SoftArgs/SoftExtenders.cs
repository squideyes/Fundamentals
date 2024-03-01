// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using Microsoft.Extensions.Configuration;
using static SquidEyes.Fundamentals.SoftStatus;

namespace SquidEyes.Fundamentals;

public static class SoftExtenders
{
    public static SoftValue<T> ToSoftValue<T>(this string input,
        Tag tag, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new SoftValue<T>(tag, null!);

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
        IEnumerable<SoftBase> values, out List<Error> errors)
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