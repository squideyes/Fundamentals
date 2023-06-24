namespace SquidEyes.Fundamentals;

public static class ConfigExtenders
{
    public static bool IsBadInput(this string value) =>
        string.IsNullOrWhiteSpace(value) || value.Any(c => !char.IsAsciiLetterOrDigit(c));
}
