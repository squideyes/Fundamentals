using ErrorOr;

namespace SquidEyes.Fundamentals;

public static class ConfigHelper
{
    public static bool TryGetErrors(string code, 
        IEnumerable<ConfigBase> values, out List<Error> errors)
    {
        errors = [];

        foreach (var value in values)
        {
            if (!value.IsValid)
                errors.Add(value.ToError(code));
        }

        return errors.Count > 0;
    }
}
