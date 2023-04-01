// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class NumericExtenders
{
    private const double DOUBLE_EPSILON = 0.00000001;
    private const float FLOAT_EPSILON = 0.00000001f;

    public static bool Approximates(this double a, double b) =>
        Math.Abs(a - b) < DOUBLE_EPSILON;

    public static bool Approximates(this float a, float b) =>
        MathF.Abs(a - b) < FLOAT_EPSILON;

    public static float Update(this float oldValue,
        float newValue, Func<float, float, bool> canUpdate)
    {
        if (canUpdate(oldValue, newValue))
            return newValue;
        else
            return oldValue;
    }

    public static double Update(this double oldValue,
        double newValue, Func<double, double, bool> canUpdate)
    {
        if (canUpdate(oldValue, newValue))
            return newValue;
        else
            return oldValue;
    }
}