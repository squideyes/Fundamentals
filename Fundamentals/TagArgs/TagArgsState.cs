namespace SquidEyes.Fundamentals;

public enum TagArgState
{
    Valid = 1,
    NotBase64,
    Invalid,
    ParseFailed,
    NotTrimmed,
    TooShort,
    TooLong,
    BadChars,
    NullOrWhitespace
}