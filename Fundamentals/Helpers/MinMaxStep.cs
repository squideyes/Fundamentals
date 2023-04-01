// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class MinMaxStep
{
    private double value;

    public MinMaxStep(
        double minimum, double maximum, double step, double value)
    {
        if (minimum != 0.0 && !double.IsNormal(minimum))
            throw new ArgumentOutOfRangeException(nameof(minimum));

        if (maximum != 0.0 && !double.IsNormal(maximum))
            throw new ArgumentOutOfRangeException(nameof(minimum));

        if (maximum < minimum)
            throw new ArgumentOutOfRangeException(nameof(maximum));

        if (step <= 0.0)
            throw new ArgumentOutOfRangeException(nameof(step));

        if (value < minimum || value > maximum)
            throw new ArgumentOutOfRangeException(nameof(value));

        Minimum = minimum;
        Maximum = maximum;
        Step = step;
        this.value = value;
    }

    public double Minimum { get; }
    public double Maximum { get; }
    public double Step { get; }

    public double Value => value;

    public override string ToString() => Value.ToString();

    public double Increment() =>
        Update(comparand => Math.Min(comparand + Step, Maximum));

    public double Decrement() =>
        Update(comparand => Math.Max(comparand - Step, Minimum));

    private double Update(Func<double, double> getNewValue)
    {
        var current = value;

        while (true)
        {
            var comparand = current;

            var newValue = getNewValue(comparand);

            current = Interlocked.CompareExchange(
                ref value, newValue, comparand);

            if (current == comparand)
                return newValue;
        }
    }
}