// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class Arg
{
    public Arg(object value)
    {
        Kind = GetArgKind(value);
        Value = value;

        if (Kind == ArgKind.Enum)
            Type = value.GetType();
    }

    public ArgKind Kind { get; }
    public Type? Type { get; }
    public object Value { get; }

    public T Get<T>() => (T)Value;

    public static ArgKind GetArgKind(object value)
    {
        if (value.GetType().IsEnum)
            return ArgKind.Enum;

        return value switch
        {
            AccountId => ArgKind.AccountId,
            bool _ => ArgKind.Boolean,
            ClientId _ => ArgKind.ClientId,
            DateTime _ => ArgKind.DateTime,
            DateOnly _ => ArgKind.DateOnly,
            double _ => ArgKind.Double,
            Email _ => ArgKind.Email,
            float _ => ArgKind.Float,
            Guid _ => ArgKind.Guid,
            int _ => ArgKind.Int32,
            long _ => ArgKind.Int64,
            Offset _ => ArgKind.Offset,
            Phone _ => ArgKind.Phone,
            Ratchet _ => ArgKind.Ratchet,
            ShortId _ => ArgKind.ShortId,
            string _ => ArgKind.String,
            TimeOnly _ => ArgKind.TimeOnly,
            TimeSpan _ => ArgKind.TimeSpan,
            Token _ => ArgKind.Token,
            Uri _ => ArgKind.Uri,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}