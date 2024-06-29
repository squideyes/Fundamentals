namespace SquidEyes.Fundamentals;

[Flags]
public enum AsciiFilter
{
    Uppers = 1,
    Lowers = 2,
    Digits = 4,
    Symbols = 8,
    AllChars = Uppers | Lowers | Digits | Symbols
}