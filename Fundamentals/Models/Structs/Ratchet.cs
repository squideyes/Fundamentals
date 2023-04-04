// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Runtime.CompilerServices;
using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct Ratchet
{
    public Offset GetWhen() => Offset.From(Value.Split('|')[0]);

    public Offset GetWhere() => Offset.From(Value.Split('|')[1]);

    public static Ratchet From(Offset when, Offset where) =>
        From($"{when}|{where}");

    public static Validation Validate(string value) =>
        VogenHelper.GetValidation<Offset>(value, IsValue);

    public static bool IsValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        var fields = value.Split('|');

        if (fields.Length != 2)
            return false;

        if (!Offset.IsValue(fields[0]))
            return false;

        if (!Offset.IsValue(fields[1]))
            return false;

        return true;
    }
}