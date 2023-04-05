// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class CreditCardHelper
{
    private static readonly Regex isAmex = GetIsAmex();
    private static readonly Regex isDiscover = GetIsDiscover();
    private static readonly Regex isMastercard = GetIsMastercard();
    private static readonly Regex isVisa = GetIsVisa();

    public static string DigitsOnly(string value) =>
        new(value.Where(char.IsDigit).ToArray());

    public static CreditCardBrand GetBrand(string value)
    {
        if (!CreditCardValidator.IsNumber(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var cardNumber = value.Where(char.IsDigit).ToArray();

        if (isAmex.IsMatch(cardNumber))
            return CreditCardBrand.Amex;
        else if (isDiscover.IsMatch(cardNumber))
            return CreditCardBrand.Discover;
        else if (isMastercard.IsMatch(cardNumber))
            return CreditCardBrand.MasterCard;
        else if (isVisa.IsMatch(cardNumber))
            return CreditCardBrand.Visa;
        else
            return CreditCardBrand.Unknown;
    }

    public static string Format(string value, bool withSpaces = true)
    {
        if (!CreditCardValidator.IsNumber(value))
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

    [GeneratedRegex("^3[47][0-9]{13}$")]
    private static partial Regex GetIsAmex();

    [GeneratedRegex("^6(?:011|5[0-9]{2})[0-9]{12}$")]
    private static partial Regex GetIsDiscover();

    [GeneratedRegex("^5[1-5][0-9]{14}$")]
    private static partial Regex GetIsMastercard();

    [GeneratedRegex("^4[0-9]{12}(?:[0-9]{3})?$")]
    private static partial Regex GetIsVisa();
}