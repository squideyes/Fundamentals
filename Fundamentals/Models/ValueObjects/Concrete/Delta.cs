namespace SquidEyes.Fundamentals;

public sealed class Delta : ValueObjectBase<Delta>
{
    public Vector Vector { get; private set; }
    public double Value { get; private set; }

    protected override void SetProperties(string input)
    {
        var fields = input.Split('=');

        Vector = Enum.Parse<Vector>(fields[0], true);
        Value = double.Parse(fields[1]);
    }

    public static bool IsInput(string input)
    {
        if (string.IsNullOrEmpty(input))
            return false;

        if (input.Any(char.IsWhiteSpace))
            return false;

        var fields = input.Split('=');

        if (fields.Length != 2)
            return false;

        if (!Enum.TryParse(fields[0], true, out Vector _))
            return false;

        if (!double.TryParse(fields[1], out double number))
            return false;

        return number > 0.0 && number <= 100.0;
    }

    public static Delta Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out Delta result) =>
        DoTryCreate(input, IsInput, out result);
}