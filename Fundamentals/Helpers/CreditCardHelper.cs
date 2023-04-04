// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class CreditCardHelper
{
    private static readonly Regex mmyyValidator = GetMMYYValidator();
    private static readonly Regex amexValidator = GetAmexValidator();
    private static readonly Regex discoverValidator = GetDiscoverValidator();
    private static readonly Regex mastercardValidator = GetMastercardValidator();
    private static readonly Regex visaValidator = GetVisaValidator();

    public static string DigitsOnly(string value) =>
        new(value.Where(char.IsDigit).ToArray());

    public static bool IsMMYY(this string value) =>
        value is not null && mmyyValidator.IsMatch(value);

    public static bool IsCVV(this string value, CreditCardBrand brand)
    {
        if (!int.TryParse(value, out int number))
            return false;

        if (number < 0)
            return false;

        switch (brand)
        {
            case CreditCardBrand.Amex:
                return number <= 9999;
            default:
                return number <= 999;
        }
    }

    public static bool IsNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var digitsOnly = DigitsOnly(value);

        int checkSum = 0;

        for (var i = digitsOnly.Length - 1; i >= 0; i -= 2)
            checkSum += digitsOnly[i] - '0';

        for (var i = digitsOnly.Length - 2; i >= 0; i -= 2)
        {
            int number = (digitsOnly[i] - '0') * 2;

            while (number > 0)
            {
                checkSum += number % 10;

                number /= 10;
            }
        }

        return (checkSum % 10) == 0;
    }

    public static CreditCardBrand GetBrand(string value)
    {
        if (!IsNumber(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var cardNumber = value.Where(char.IsDigit).ToArray();

        if (amexValidator.IsMatch(cardNumber))
            return CreditCardBrand.Amex;
        else if (discoverValidator.IsMatch(cardNumber))
            return CreditCardBrand.Discover;
        else if (mastercardValidator.IsMatch(cardNumber))
            return CreditCardBrand.MasterCard;
        else if (visaValidator.IsMatch(cardNumber))
            return CreditCardBrand.Visa;
        else
            return CreditCardBrand.Unknown;
    }

    public static string Format(string value, bool withSpaces = true)
    {
        if (!IsNumber(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var digitsOnly = DigitsOnly(value);

        if (!withSpaces)
            return digitsOnly;

        string Format(params int[] lengths)
        {
            var start = 0;

            var sb = new StringBuilder();

            foreach (var length in lengths)
            {
                if (sb.Length > 0)
                    sb.Append(' ');

                sb.Append(digitsOnly.AsSpan(start, length));

                start += length;
            }

            return sb.ToString();
        }

        return GetBrand(digitsOnly) switch
        {
            CreditCardBrand.Unknown => digitsOnly,
            CreditCardBrand.Amex => Format(4, 6, 5),
            _ => Format(4, 4, 4, 4)
        };
    }

    [GeneratedRegex("^(0[1-9]|1[012])(2[3-9]|3[0-9])$")]
    private static partial Regex GetMMYYValidator();

    [GeneratedRegex("^3[47][0-9]{13}$")]
    private static partial Regex GetAmexValidator();

    [GeneratedRegex("^6(?:011|5[0-9]{2})[0-9]{12}$")]
    private static partial Regex GetDiscoverValidator();

    [GeneratedRegex("^5[1-5][0-9]{14}$")]
    private static partial Regex GetMastercardValidator();

    [GeneratedRegex("^4[0-9]{12}(?:[0-9]{3})?$")]
    private static partial Regex GetVisaValidator();
}