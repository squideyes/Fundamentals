// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static SquidEyes.Fundamentals.ConfigStatus;

namespace SquidEyes.Fundamentals;

public static class ConfigExtenders
{
    public static ConfigValue<T> ToConfigValue<T>(
        this string input, Tag tag, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        return input.ToConfigValue(tag, false, isValid);
    }

    public static ConfigValue<T> ToConfigValue<T>(this string input,
        Tag tag, bool isOptional, Func<T, bool> isValid = null!)
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

    public static ConfigString ToConfigString(
        this string input, Tag tag, Func<string, bool> isValid = null!)
    {
        return input.ToConfigString(tag, false, isValid);
    }
    
    public static ConfigString ToConfigString(this string input, 
        Tag tag, bool isOptional, Func<string, bool> isValid = null!)
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

    public static ConfigUri ToConfigUri(
        this string input, Tag tag, Func<Uri, bool> isValid = null!)
    {
        return input.ToConfigUri(tag, false, UriKind.Absolute, isValid);
    }

    public static ConfigUri ToConfigUri(this string input, Tag tag,
        bool isOptional, UriKind uriKind = UriKind.Absolute,
            Func<Uri, bool> isValid = null!)
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

    public static ConfigEnum<T> ToConfigEnum<T>(
        this string input, Tag tag, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        return input.ToConfigEnum(tag, false, isValid);
    }

    public static ConfigEnum<T> ToConfigEnum<T>(this string input,
        Tag tag, bool isOptional, Func<T, bool> isValid = null!)
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

    public static ConfigEmail ToConfigEmail(
        this string input, Tag tag, Func<Email, bool> isValid = null!)
    {
        return input.ToConfigEmail(tag, false, isValid);
    }

    public static ConfigEmail ToConfigEmail(this string input,
        Tag tag, bool isOptional, Func<Email, bool> isValid = null!)
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

    public static ConfigPhone ToConfigPhone(
        this string input, Tag tag, Func<Phone, bool> isValid = null!)
    {
        return input.ToConfigPhone(tag, false, isValid);
    }

    public static ConfigPhone ToConfigPhone(this string input, 
        Tag tag, bool isOptional, Func<Phone, bool> isValid = null!)
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

    //////////////////////////

    public static ConfigMultiTag ToConfigMultiTag(
        this string input, Tag tag, Func<MultiTag, bool> isValid = null!)
    {
        return input.ToConfigMultiTag(tag, false, isValid);
    }

    public static ConfigMultiTag ToConfigMultiTag(this string input,
        Tag tag, bool isOptional, Func<MultiTag, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigMultiTag(tag, null!);

            return new ConfigMultiTag(tag, input!, NullOrEmpty);
        }

        if (!MultiTag.TryCreate(input, out MultiTag? multiTag))
            return new ConfigMultiTag(tag, input, ParseError);

        if (isValid is not null && !isValid(multiTag))
            return new ConfigMultiTag(tag, input, NotValid);

        return new ConfigMultiTag(tag, multiTag);
    }

    //////////////////////////

    public static ConfigTag ToConfigTag(
        this string input, Tag tag, Func<Tag, bool> isValid = null!)
    {
        return input.ToConfigTag(tag, false, isValid);
    }

    public static ConfigTag ToConfigTag(this string input,
        Tag tag, bool isOptional, Func<Tag, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigTag(tag, null!);

            return new ConfigTag(tag, input!, NullOrEmpty);
        }

        if (!Tag.TryCreate(input, out Tag? data))
            return new ConfigTag(tag, input, ParseError);

        if (isValid is not null && !isValid(data))
            return new ConfigTag(tag, input, NotValid);

        return new ConfigTag(tag, data);
    }


    //////////////////////////

    public static ConfigActorId ToConfigActorId(
        this string input, Tag tag, Func<ActorId, bool> isValid = null!)
    {
        return input.ToConfigActorId(tag, false, isValid);
    }

    public static ConfigActorId ToConfigActorId(this string input,
        Tag tag, bool isOptional, Func<ActorId, bool> isValid = null!)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            if (isOptional)
                return new ConfigActorId(tag, null!);

            return new ConfigActorId(tag, input!, NullOrEmpty);
        }

        if (!ActorId.TryCreate(input, out ActorId? actorId))
            return new ConfigActorId(tag, input, ParseError);

        if (isValid is not null && !isValid(actorId))
            return new ConfigActorId(tag, input, NotValid);

        return new ConfigActorId(tag, actorId);
    }
}