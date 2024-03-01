// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using static SquidEyes.Fundamentals.ConfigStatus;

namespace SquidEyes.Fundamentals;

public static class ConfigExtenders
{
    public static ConfigValue<T> ToConfigValue<T>(this string input,
        Tag tag, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigValue<T>(tag, null!);

            return new ConfigValue<T>(tag, input!, NullOrEmpty);
        }

        if (!T.TryParse(input, null, out T value))
            return new ConfigValue<T>(tag, input, ParseError);

        if (isValid is not null && !isValid(value))
            return new ConfigValue<T>(tag, input, NotValid);

        return new ConfigValue<T>(tag, value);
    }

    //////////////////////////

    public static ConfigString ToConfigString(this string input, Tag tag,
        bool isOptional = false, Func<string, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigString(tag, null!);

            return new ConfigString(tag, input!, NullOrEmpty);
        }

        if (isValid is not null && !isValid(input))
            return new ConfigString(tag, input, NotValid);

        return new ConfigString(tag, input);
    }

    //////////////////////////

    public static ConfigUri ToConfigUri(this string input, Tag tag,
        UriKind uriKind = UriKind.Absolute,
        bool isOptional = false, Func<Uri, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigUri(tag, null!);

            return new ConfigUri(tag, input!, NullOrEmpty);
        }

        if (!Uri.TryCreate(input, uriKind, out Uri? uri))
            return new ConfigUri(tag, input, ParseError);

        if (isValid is not null && !isValid(uri))
            return new ConfigUri(tag, input, NotValid);

        return new ConfigUri(tag, uri);
    }

    //////////////////////////

    public static ConfigEnum<T> ToConfigEnum<T>(this string input,
        Tag tag, bool isOptional = true, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigEnum<T>(tag, null!);

            return new ConfigEnum<T>(tag, input!, NullOrEmpty);
        }

        if (!Enum.TryParse<T>(input, out var value))
            return new ConfigEnum<T>(tag, input, ParseError);

        if (isValid is not null && !isValid(value))
            return new ConfigEnum<T>(tag, input, NotValid);

        return new ConfigEnum<T>(tag, value);
    }

    //////////////////////////

    public static ConfigEmail ToConfigEmail(this string input, Tag tag,
        bool isOptional = false, Func<Email, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigEmail(tag, null!);

            return new ConfigEmail(tag, input!, NullOrEmpty);
        }

        if (!Email.TryCreate(input, out Email? email))
            return new ConfigEmail(tag, input, ParseError);

        if (isValid is not null && !isValid(email))
            return new ConfigEmail(tag, input, NotValid);

        return new ConfigEmail(tag, email);
    }

    //////////////////////////

    public static ConfigPhone ToConfigPhone(this string input, Tag tag,
        bool isOptional = false, Func<Phone, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigPhone(tag, null!);

            return new ConfigPhone(tag, input!, NullOrEmpty);
        }

        if (!Phone.TryCreate(input, out Phone? phone))
            return new ConfigPhone(tag, input, ParseError);

        if (isValid is not null && !isValid(phone))
            return new ConfigPhone(tag, input, NotValid);

        return new ConfigPhone(tag, phone);
    }
}