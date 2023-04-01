// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class FloatExtenders
{
    public static int GetDigits(this float value)
    {
        var precision = 0;

        var number = Math.Round(value, 8);

        while (number * Math.Pow(10, precision) !=
            Math.Round(number * Math.Pow(10, precision)))
        {
            precision++;

            precision.Must().BeLessThan(8);
        }

        return precision;
    }
}