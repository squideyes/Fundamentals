// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.RegularExpressions;

namespace SquidEyes.Fundamentals;

public static partial class CreditCardValidator
{
    private static readonly Regex mmyyValidator = GetMMYYValidator();

    public static bool IsMMYY(string value) =>
        value is not null && mmyyValidator.IsMatch(value);

    public static bool IsCVV(string value, CreditCardBrand brand)
    {
        if (!int.TryParse(value, out int number))
            return false;

        if (number < 0)
            return false;

        if (brand == CreditCardBrand.Amex)
            return number <= 9999;
        else
            return number <= 999;
    }

    public static bool IsNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var digitsOnly = CreditCardHelper.DigitsOnly(value);

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

    [GeneratedRegex("^(0[1-9]|1[012])(2[3-9]|3[0-9])$")]
    private static partial Regex GetMMYYValidator();
}