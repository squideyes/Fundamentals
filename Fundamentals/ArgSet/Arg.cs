// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

internal class Arg
{
    public Arg(object value)
    {
        Kind = GetArgKind(value);
        Value = value;

        switch (Kind)
        {
            case ArgKind.Custom:
            case ArgKind.Enum:
                Type = value.GetType();
                break;
        }
    }

    public ArgKind Kind { get; }
    public object Value { get; }
    public Type? Type { get; }

    public static ArgKind GetArgKind(object value)
    {
        if (value.GetType().IsEnum)
            return ArgKind.Enum;

        return value switch
        {
            bool _ => ArgKind.Boolean,
            ClientId _ => ArgKind.ClientId,
            ConfigKey _ => ArgKind.ConfigKey,
            DateTime _ => ArgKind.DateTime,
            DateOnly _ => ArgKind.DateOnly,
            double _ => ArgKind.Double,
            Email _ => ArgKind.Email,
            float _ => ArgKind.Float,
            Guid _ => ArgKind.Guid,
            int _ => ArgKind.Int32,
            long _ => ArgKind.Int64,
            Phone _ => ArgKind.Phone,
            ShortId _ => ArgKind.ShortId,
            string _ => ArgKind.String,
            TimeOnly _ => ArgKind.TimeOnly,
            TimeSpan _ => ArgKind.TimeSpan,
            Token _ => ArgKind.Token,
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
    }
}