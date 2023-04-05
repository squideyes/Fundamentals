// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class PostalCodeValidator
{
    private static readonly Dictionary<string, List<string>> patterns = GetPatterns();

    private static Dictionary<string, List<string>> GetPatterns()
    {
        var keyValues = new Dictionary<string, string>()
        {
            { "US", "99999" },
            { "US", "99999-9999" },
            { "CA", "A9A 9A9" },
            { "CA", "A9A-9A9" },
            { "UK", "AAA 9AA" },
            { "UK", "AA99 9AA" },
            { "UK", "AA9 9AA" },
            { "AR", "A9999AAA" },
            { "AU", "9999" },
            { "AT", "9999" },
            { "BE", "9999" },
            { "BR", "99999" },
            { "BR", "99999-999" },
            { "BG", "9999" },
            { "KH", "99999" },
            { "CL", "9999999" },
            { "CL", "999-9999" },
            { "CO", "999999" },
            { "HR", "99999" },
            { "Cr", "99999" },
            { "Cr", "99999-9999" },
            { "CZ", "999 99" },
            { "CZ", "999-99" },
            { "CZ", "99999" },
            { "DK", "9999" },
            { "DK", "99" },
            { "DO", "99999" },
            { "EC", "999999" },
            { "EG", "99999" },
            { "EE", "99999" },
            { "FI", "99999" },
            { "FR", "99999" },
            { "DE", "99999" },
            { "GR", "99 99" },
            { "GT", "99999" },
            { "HU", "9999" },
            { "IN", "999 999" },
            { "IN", "999999" },
            { "ID", "99999" },
            { "IE", "A99 A9A9" },
            { "IE", "A99 AA99" },
            { "IE", "A99 A9AA" },
            { "IT", "99999" },
            { "JO", "99999" },
            { "KW", "99999" },
            { "LV", "9999" },
            { "LT", "99999" },
            { "LU", "99999" },
            { "MY", "99999" },
            { "MT", "AAA 9999" },
            { "MX", "99999" },
            { "ME", "99999" },
            { "NL", "9999 AA" },
            { "NL", "9999AA" },
            { "NZ", "9999" },
            { "NO", "9999" },
            { "OM", "999" },
            { "PK", "99999" },
            { "PY", "9999" },
            { "PE", "99999" },
            { "PH", "9999" },
            { "PL", "99-999" },
            { "PT", "9999-999" },
            { "RO", "999999" },
            { "RU", "999999" },
            { "SA", "99999-9999" },
            { "RS", "99999" },
            { "SG", "999999" },
            { "SK", "99999" },
            { "SK", "999 99" },
            { "SI", "9999" },
            { "ZA", "9999" },
            { "KR", "99999" },
            { "ES", "99999" },
            { "SE", "SE-999-99" },
            { "SE", "999-99" },
            { "SE", "999 99" },
            { "SE", "99999" },
            { "CH", "9999" },
            { "TW", "999" },
            { "TW", "999999" },
            { "TW", "999-999" },
            { "TZ", "99999" },
            { "TH", "99999" },
            { "TT", "999999" },
            { "TN", "9999" },
            { "TR", "99999" },
            { "UA", "99999" },
            { "UY", "99999" },
            { "VE", "9999" },
            { "VE", "9999-A" },
            { "VN", "999999" }
        };

        var codes = new Dictionary<string, List<string>>();

        foreach (var kv in keyValues)
        {
            if (!codes.ContainsKey(kv.Key))
                codes.Add(kv.Key, new List<string>());

            codes[kv.Key].Add(kv.Value);
        }

        return codes;
    }

    public static bool IsPostalCode(string value, string country)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (CountryValidator.IsCountryCode(country))
            return false;

        if (!patterns.ContainsKey(country))
            throw new ArgumentOutOfRangeException(nameof(country));

        foreach (var patterns in patterns[country])
        {
            if (value.Length != patterns.Length)
                continue;

            var isValid = true;

            for (int i = 0; i < patterns.Length; i++)
            {
                var p = patterns[i];
                var v = value[i];

                var isMatch = p switch
                {
                    'A' => char.IsAsciiLetterUpper(v),
                    '9' => char.IsDigit(v),
                    _ => v == p
                };

                if (!isMatch)
                {
                    isValid = false;

                    break;
                }
            }

            if (isValid)
                return true;
        }

        return false;
    }
}