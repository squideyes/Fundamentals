using static SquidEyes.Fundamentals.TagArgState;
using static SquidEyes.Fundamentals.AsciiFilter;
using TAAK = SquidEyes.Fundamentals.TagArgArgKind;

namespace SquidEyes.Fundamentals;

public static class TagArgExtenders
{
    public static TagArg<char> ToCharTagArg(
        this string input, Tag tag, Func<char, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Char, isValid);
    }

    public static TagArg<int> ToInt32TagArg(
        this string input, Tag tag, Func<int, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Int32, isValid);
    }

    public static TagArg<short> ToInt16TagArg(
        this string input, Tag tag, Func<short, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Int16, isValid);
    }

    public static TagArg<byte> ToByteTagArg(
        this string input, Tag tag, Func<byte, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Byte, isValid);
    }

    public static TagArg<float> ToFloatTagArg(
        this string input, Tag tag, Func<float, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Float, isValid);
    }

    public static TagArg<long> ToInt64TagArg(
        this string input, Tag tag, Func<long, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Int64, isValid);
    }

    public static TagArg<double> ToDoubleTagArg(
        this string input, Tag tag, Func<double, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Double, isValid);
    }

    public static TagArg<bool> ToBoolTagArg(
        this string input, Tag tag, Func<bool, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.Bool, isValid);
    }

    public static TagArg<DateTime> ToDateTimeTagArg(
        this string input, Tag tag, Func<DateTime, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.DateTime, isValid);
    }

    public static TagArg<DateOnly> ToDateOnlyTagArg(
        this string input, Tag tag, Func<DateOnly, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.DateOnly, isValid);
    }

    public static TagArg<TimeOnly> ToTimeOnlyTagArg(
        this string input, Tag tag, Func<TimeOnly, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.TimeOnly, isValid);
    }

    public static TagArg<TimeSpan> ToTimeSpanTagArg(
        this string input, Tag tag, Func<TimeSpan, bool> isValid = null!)
    {
        return input.ToParseableTagArg(tag, TAAK.TimeSpan, isValid);
    }

    public static TagArg<string> ToBase64TagArg(
        this string input, Tag tag, Func<string, bool> isValid = null!)
    {
        if (TagArg<string>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!input.IsBase64String())
            return new TagArg<string>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(input))
            return new TagArg<string>(tag, input, Invalid);
        else
            return new TagArg<string>(tag, input, TAAK.Base64);
    }

    public static TagArg<string> ToTextLineTagArg(this string input, Tag tag, 
        bool isRequired = true, bool mustBeTrimmed = true, int minLength = 1,
            int maxLength = 100, AsciiFilter filter = AllChars,
                Func<string, bool> isValid = null!)
    {
        return TagArg<string>.Create(tag, input!, isRequired, 
            mustBeTrimmed, minLength, maxLength, filter, isValid);
    }

    public static TagArg<Email> ToEmailTagArg(
        this string input, Tag tag, Func<Email, bool> isValid = null!)
    {
        if (TagArg<Email>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Email.TryCreate(input, out Email value))
            return new TagArg<Email>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Email>(tag, input, Invalid);
        else
            return new TagArg<Email>(tag, value, TAAK.Email);
    }

    public static TagArg<T> ToEnumTagArg<T>(this string input,
        Tag tag, bool ignoreCase = false, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        if (TagArg<T>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Enum.TryParse(input, ignoreCase, out T value))
            return new TagArg<T>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, Invalid);
        else
            return new TagArg<T>(tag, value, TAAK.Enum);
    }

    public static TagArg<Uri> ToUriTagArg(this string input, Tag tag,
        UriKind kind = UriKind.Absolute, Func<Uri, bool> isValid = null!)
    {
        if (TagArg<Uri>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Uri.TryCreate(input, kind, out Uri? value))
            return new TagArg<Uri>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Uri>(tag, input, Invalid);
        else
            return new TagArg<Uri>(tag, value, TAAK.Uri);
    }

    public static TagArg<Guid> ToGuidTagArg(
        this string input, Tag tag, Func<Guid, bool> isValid = null!)
    {
        if (TagArg<Guid>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Guid.TryParse(input, out Guid value))
            return new TagArg<Guid>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Guid>(tag, input, Invalid);
        else
            return new TagArg<Guid>(tag, value, TAAK.Guid);
    }

    public static TagArg<Phone> ToPhoneTagArg(
        this string input, Tag tag, Func<Phone, bool> isValid = null!)
    {
        if (TagArg<Phone>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Phone.TryCreate(input, out Phone value))
            return new TagArg<Phone>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Phone>(tag, input, Invalid);
        else
            return new TagArg<Phone>(tag, value, TAAK.Phone);
    }

    public static TagArg<Tag> ToTagTagArg(
        this string input, Tag tag, Func<Tag, bool> isValid = null!)
    {
        if (TagArg<Tag>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!Tag.TryCreate(input, out Tag value))
            return new TagArg<Tag>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Tag>(tag, input, Invalid);
        else
            return new TagArg<Tag>(tag, value, TAAK.Tag);
    }

    public static TagArg<MultiTag> ToMultiTagArg(
        this string input, Tag tag, Func<MultiTag, bool> isValid = null!)
    {
        if (TagArg<MultiTag>.IsNullOrWhiteSpace(input, tag, out var tagArg))
            return tagArg;
        else if (!MultiTag.TryCreate(input, out MultiTag value))
            return new TagArg<MultiTag>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<MultiTag>(tag, input, Invalid);
        else
            return new TagArg<MultiTag>(tag, value, TAAK.MultiTag);
    }

    private static TagArg<T> ToParseableTagArg<T>(
        this string input, Tag tag, TAAK kind, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (TagArg<T>.IsNullOrWhiteSpace(input, tag, out var arg))
            return arg;
        else if (!T.TryParse(input, null, out T value))
            return new TagArg<T>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, Invalid);
        else
            return new TagArg<T>(tag, value, kind);
    }
}
