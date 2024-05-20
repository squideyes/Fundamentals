// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text;
using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class CardHelper
{
    private static readonly Regex isAmex = GetIsAmex();
    private static readonly Regex isDiscover = GetIsDiscover();
    private static readonly Regex isMastercard = GetIsMastercard();
    private static readonly Regex isVisa = GetIsVisa();

    public static string DigitsOnly(string value) =>
        new(value.Where(char.IsDigit).ToArray());

    public static bool TryGetBrand(string value, out CardBrand? brand)
    {
        brand = null;

        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (!value.All(c => char.IsDigit(c) || c == ' '))
            return false;

        var cardNumber = value.Where(char.IsDigit).ToArray();

        if (isAmex.IsMatch(cardNumber))
            brand = CardBrand.Amex;
        else if (isDiscover.IsMatch(cardNumber))
            brand = CardBrand.Discover;
        else if (isVisa.IsMatch(cardNumber))
            brand = CardBrand.Visa;
        else if (isMastercard.IsMatch(cardNumber))
            brand = CardBrand.MasterCard;

        return brand.HasValue;
    }

    public static string Format(string value, bool withSpaces = true)
    {
        if (!CardValidator.IsNumber(value))
            throw new ArgumentOutOfRangeException(nameof(value));

        var digitsOnly = DigitsOnly(value);

        if (!TryGetBrand(value, out var brand))
            return digitsOnly;

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

        return brand switch
        {
            CardBrand.Amex => Format(4, 6, 5),
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