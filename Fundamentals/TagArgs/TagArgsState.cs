namespace SquidEyes.Fundamentals;

public enum TagArgState
{
    Valid = 1,
    Invalid,
    ParseFailed,
    NotTrimmed,
    TooLong,
    BadChars,
    NullOrWhitespace
}