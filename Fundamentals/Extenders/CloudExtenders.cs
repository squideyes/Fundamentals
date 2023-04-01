// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class CloudExtenders
{
    private static readonly Regex dnsNameValidator = GetDnsNameValidator();

    public static bool IsDnsName(this string value) =>
        dnsNameValidator.IsMatch(value);

    public static bool IsBlobName(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length < 1 || value.Length > 255)
            return false;

        if (value.StartsWith('/') || value.EndsWith('/'))
            return false;

        foreach (var part in value.Split('/'))
        {
            if (part.Length == 0)
                return false;

            if (!part.All(char.IsAsciiLetterOrDigit))
                return false;
        }

        return true;
    }

    [GeneratedRegex("^[a-z0-9](?!.*--)[a-z0-9-]{1,61}[a-z0-9]$", RegexOptions.Compiled)]
    private static partial Regex GetDnsNameValidator();
}