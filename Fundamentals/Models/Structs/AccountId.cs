// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Vogen;

namespace SquidEyes.Fundamentals;

[ValueObject<string>]
public readonly partial struct AccountId
{
    public ClientId GetClientId() => ClientId.From(Value[0..8]);

    public char GetCode() => Value[9];

    public int GetOrdinal() => int.Parse(Value[10..^1]);

    public static AccountId From(ClientId clientId, char code, int ordinal) =>
        From($"{clientId}({code}{ordinal:000})");

    public static Validation Validate(string value)=>
        VogenHelper.GetValidation<AccountId>(value, IsValue);

    public static bool IsValue(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        if (value.Length != 14)
            return false;

        if (!ClientId.IsValue(value[0..8]))
            return false;

        var code = value[9];

        if (!char.IsAsciiLetterUpper(code))
            return false;

        if (!int.TryParse(value[10..^1], out int number))
            return false;

        return number.IsBetween(1, 999);
    }
}