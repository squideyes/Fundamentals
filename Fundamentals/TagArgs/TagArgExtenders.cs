using static SquidEyes.Fundamentals.TagArgState;

namespace SquidEyes.Fundamentals;

public static class TagArgExtenders
{
    public static TagArg<T> ToParseableArg<T>(this string input,
        Tag tag, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (TagArg<T>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!T.TryParse(input, null, out T value))
            return new TagArg<T>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, NotValid);
        else
            return new TagArg<T>(tag, value);
    }

    public static TagArg<string> ToStringArg(this string input,
        Tag tag, bool isOptional = false, Func<string, bool> isValid = null!)
    {
        if (TagArg<string>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (isValid is not null && !isValid(input))
            return new TagArg<string>(tag, input, NotValid);
        else
            return new TagArg<string>(tag, input);
    }

    public static TagArg<Email> ToEmailArg(this string input, 
        Tag tag, bool isOptional = false, Func<Email, bool> isValid = null!)
    {
        if (TagArg<Email>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!Email.TryCreate(input, out Email value))
            return new TagArg<Email>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Email>(tag, input, NotValid);
        else
            return new TagArg<Email>(tag, value);
    }

    public static TagArg<T> ToEnumArg<T>(this string input, Tag tag, 
        bool ignoreCase = false, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        if (TagArg<T>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!Enum.TryParse(input, ignoreCase, out T value))
            return new TagArg<T>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, NotValid);
        else
            return new TagArg<T>(tag, value);
    }

    public static TagArg<Uri> ToUriArg(this string input, Tag tag, 
        UriKind kind = UriKind.Absolute, bool isOptional = false, Func<Uri, bool> isValid = null!)
    {
        if (TagArg<Uri>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!Uri.TryCreate(input, kind, out Uri? value))
            return new TagArg<Uri>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Uri>(tag, input, NotValid);
        else
            return new TagArg<Uri>(tag, value);
    }

    public static TagArg<Phone> ToPhoneArg(this string input, 
        Tag tag, bool isOptional = false, Func<Phone, bool> isValid = null!)
    {
        if (TagArg<Phone>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!Phone.TryCreate(input, out Phone value))
            return new TagArg<Phone>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Phone>(tag, input, NotValid);
        else
            return new TagArg<Phone>(tag, value);
    }

    public static TagArg<Tag> ToTagArg(this string input, 
        Tag tag, bool isOptional = false, Func<Tag, bool> isValid = null!)
    {
        if (TagArg<Tag>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!Tag.TryCreate(input, out Tag value))
            return new TagArg<Tag>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Tag>(tag, input, NotValid);
        else
            return new TagArg<Tag>(tag, value);
    }

    public static TagArg<MultiTag> ToMultiTagArg(this string input, 
        Tag tag, bool isOptional = false, Func<MultiTag, bool> isValid = null!)
    {
        if (TagArg<MultiTag>.IsNullOrEmpty(input, tag, isOptional, out var arg))
            return arg;
        else if (!MultiTag.TryCreate(input, out MultiTag value))
            return new TagArg<MultiTag>(tag, input, ParseError);
        else if (isValid is not null && !isValid(value))
            return new TagArg<MultiTag>(tag, input, NotValid);
        else
            return new TagArg<MultiTag>(tag, value);
    }
}
