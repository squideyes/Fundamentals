using static SquidEyes.Fundamentals.TagArgState;
using static SquidEyes.Fundamentals.AsciiFilter;

namespace SquidEyes.Fundamentals;

public static class TagArgExtenders
{
    private static readonly HashSet<char> keyboardSymbols = new(
        "`~!@#$%^&*()_-+=[]{}\\|:;'?/.<>,'".ToCharArray());

    public static TagArg<T> ToParseableTagArg<T>(this string input,
        Tag tag, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, IParsable<T>
    {
        if (TagArg<T>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!T.TryParse(input, null, out T value))
            return new TagArg<T>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, Invalid);
        else
            return new TagArg<T>(tag, value);
    }

    public static TagArg<string> ToBase64TagArg(this string input,
        Tag tag, bool isOptional = false, Func<string, bool> isValid = null!)
    {
        if (TagArg<string>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!input.IsBase64String())
            return new TagArg<string>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(input))
            return new TagArg<string>(tag, input, Invalid);
        else
            return new TagArg<string>(tag, input);
    }

    public static TagArg<string> ToAsciiTagArg(this string input, Tag tag,bool isOptional = false, 
        bool isTrimmed = true, int minLength = 1, int maxLength = 100, AsciiFilter filter = AllChars, 
        Func<string, bool> isValid = null!, Func<string, string> fixUp = null!)
    {
        minLength.MustBe().Positive();
        maxLength.MustBe().GreaterThanOrEqualTo(minLength);

        bool IsValid(char c)
        {
            bool isValid = false;

            if (char.IsAsciiLetterLower(c) && (filter & Lowers) == Lowers)
                isValid = true;
            else if (char.IsAsciiLetterUpper(c) && (filter & Uppers) == Uppers)
                isValid = true;
            else if (char.IsDigit(c) && (filter & Digits) == Digits)
                isValid = true;
            else if (keyboardSymbols.Contains(c) && (filter & Symbols) == Symbols)
                isValid = true;

            return isValid;
        }

        if (fixUp is not null)
            input = fixUp(input);

        if (TagArg<string>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (input.Length < minLength)
            return new TagArg<string>(tag, input, TooShort, minLength);
        else if (input.Length > maxLength)
            return new TagArg<string>(tag, input, TooShort, maxLength);
        else if (isTrimmed && !(char.IsWhiteSpace(input[0]) || !char.IsWhiteSpace(input[^1])))
            return new TagArg<string>(tag, input, NotTrimmed);
        else if (!input.All(IsValid))
            return new TagArg<string>(tag, input, BadChars);
        else if (isValid is not null && !isValid(input))
            return new TagArg<string>(tag, input, Invalid);
        else
            return new TagArg<string>(tag, input);
    }

    public static TagArg<Email> ToEmailArg(this string input,
        Tag tag, bool isOptional = false, Func<Email, bool> isValid = null!)
    {
        if (TagArg<Email>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!Email.TryCreate(input, out Email value))
            return new TagArg<Email>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Email>(tag, input, Invalid);
        else
            return new TagArg<Email>(tag, value);
    }

    public static TagArg<T> ToEnumArg<T>(this string input, Tag tag,
        bool ignoreCase = false, bool isOptional = false, Func<T, bool> isValid = null!)
            where T : struct, Enum
    {
        if (TagArg<T>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!Enum.TryParse(input, ignoreCase, out T value))
            return new TagArg<T>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<T>(tag, input, Invalid);
        else
            return new TagArg<T>(tag, value);
    }

    public static TagArg<Uri> ToUriArg(this string input, Tag tag,
        UriKind kind = UriKind.Absolute, bool isOptional = false, Func<Uri, bool> isValid = null!)
    {
        if (TagArg<Uri>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!Uri.TryCreate(input, kind, out Uri? value))
            return new TagArg<Uri>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Uri>(tag, input, Invalid);
        else
            return new TagArg<Uri>(tag, value);
    }

    public static TagArg<Phone> ToPhoneArg(this string input,
        Tag tag, bool isOptional = false, Func<Phone, bool> isValid = null!)
    {
        if (TagArg<Phone>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!Phone.TryCreate(input, out Phone value))
            return new TagArg<Phone>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Phone>(tag, input, Invalid);
        else
            return new TagArg<Phone>(tag, value);
    }

    public static TagArg<Tag> ToTagArg(this string input,
        Tag tag, bool isOptional = false, Func<Tag, bool> isValid = null!)
    {
        if (TagArg<Tag>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!Tag.TryCreate(input, out Tag value))
            return new TagArg<Tag>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<Tag>(tag, input, Invalid);
        else
            return new TagArg<Tag>(tag, value);
    }

    public static TagArg<MultiTag> ToMultiTagArg(this string input,
        Tag tag, bool isOptional = false, Func<MultiTag, bool> isValid = null!)
    {
        if (TagArg<MultiTag>.IsNullOrWhiteSpace(input, tag, isOptional, out var arg))
            return arg;
        else if (!MultiTag.TryCreate(input, out MultiTag value))
            return new TagArg<MultiTag>(tag, input, ParseFailed);
        else if (isValid is not null && !isValid(value))
            return new TagArg<MultiTag>(tag, input, Invalid);
        else
            return new TagArg<MultiTag>(tag, value);
    }
}
