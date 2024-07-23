namespace SquidEyes.Fundamentals;

[Flags]
public enum AsciiFilter
{
    Uppers = 1,
    Lowers = 2,
    Digits = 4,
    Spaces = 8,
    Symbols = 16,
    Alphanumeric = Uppers | Lowers | Digits,
    AllChars = Uppers | Lowers | Digits | Spaces | Symbols
}