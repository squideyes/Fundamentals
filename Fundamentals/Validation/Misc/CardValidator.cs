// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class CardValidator
{
    public static bool IsCVC(int value, CardBrand brand)
    {
        if (value < 0)
            return false;

        if (brand == CardBrand.Amex)
            return value <= 9999;
        else
            return value <= 999;
    }

    public static bool IsNumber(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var digitsOnly = CardHelper.DigitsOnly(value);

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
}