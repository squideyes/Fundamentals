// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Offset
{
    public Vector GetVector() => Value[0..4].ToEnumValue<Vector>();

    public double GetDelta() => double.Parse(Value[5..]);

    public static Offset From(Vector vector, double delta) =>
        From($"{vector}={delta}");

    public static Validation Validate(string value)=>
        VogenHelper.GetValidation<Offset>(value, IsValue);

    public static bool IsValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        if (value.Length < 6)
            return false;

        if (!Enum.TryParse(value[0..4], out Vector _))
            return false;

        if (!double.TryParse(value[5..], out double number))
            return false;

        return number > 0.0 && number < 1000.0;
    }
}