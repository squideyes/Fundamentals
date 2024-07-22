// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static System.StringSplitOptions;

namespace SquidEyes.Fundamentals;

internal class EmailValidator
{
    // DATA
    // "https://raw.githubusercontent.com/disposable-email-domains/disposable-email-domains/master/disposable_email_blocklist.conf";
    // "https://data.iana.org/TLD/tlds-alpha-by-domain.txt";

    private readonly HashSet<string> blockedDomains;
    private readonly HashSet<string> topLevelDomains;

    public EmailValidator()
    {
        blockedDomains = Parse(Domains.Blocked, "//");
        topLevelDomains = Parse(Domains.TopLevel, "#");
    }

    public bool IsValid(string? value, int maxLength)
    {
        if (!IsEmailAddress(value, maxLength))
            return false;

        var domain = value!.Split('@')[1];

        return !blockedDomains.Contains(
            domain, StringComparer.OrdinalIgnoreCase);
    }

    private static HashSet<string> Parse(string input, string commentPrefix)
    {
        return new HashSet<string>(input!.Split(["\r\n", "\r", "\n"], 
            RemoveEmptyEntries | TrimEntries)
                .Where(l => !l.StartsWith(commentPrefix)));
    }

    private bool IsEmailAddress(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length > maxLength)
            return false;

        var fields = value.Split('@');

        if (fields.Length != 2)
            return false;

        if (string.IsNullOrWhiteSpace(fields[0]))
            return false;

        if (string.IsNullOrWhiteSpace(fields[1]))
            return false;

        bool HasGoodParts(string field, int minParts, bool lastIsTld)
        {
            var parts = field.Split('.');

            if (parts.Length < minParts)
                return false;

            foreach (var part in parts.Take(
                parts.Length - (lastIsTld ? 1 : 0)))
            {
                if (part.Length == 0)
                    return false;

                if (!char.IsAsciiLetter(part[0]))
                    return false;

                for (int i = 1; i < part.Length - 1; i++)
                {
                    var c = part[i];

                    if (c != '-' && !char.IsLetterOrDigit(c))
                        return false;
                }

                if (!char.IsAsciiLetterOrDigit(part[^1]))
                    return false;
            }

            if (lastIsTld)
            {
                return topLevelDomains.Contains(parts[^1],
                    StringComparer.OrdinalIgnoreCase);
            }
            else
            {
                return true;
            }
        }

        if (!HasGoodParts(fields[0], 1, false))
            return false;

        if (!HasGoodParts(fields[1], 2, true))
            return false;

        return true;
    }
}