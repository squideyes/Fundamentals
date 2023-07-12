// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringArgSetConverter : JsonConverter<ArgSet>
{
    public override void Write(Utf8JsonWriter writer,
        ArgSet value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var kv in value.Args)
        {
            writer.WritePropertyName(kv.Key.ToString());

            writer.WriteStartObject();

            writer.WriteString("Kind", kv.Value.Kind.ToString());

            if (kv.Value.Kind == ArgKind.Enum)
                writer.WriteString("Type", kv.Value.Type!.ToString());

            switch (kv.Value.Kind)
            {
                case ArgKind.Boolean:
                    writer.WriteBoolean("Value", (bool)kv.Value.Value);
                    break;
                case ArgKind.Double:
                    writer.WriteNumber("Value", (double)kv.Value.Value);
                    break;
                case ArgKind.Float:
                    writer.WriteNumber("Value", (float)kv.Value.Value);
                    break;
                case ArgKind.Int32:
                    writer.WriteNumber("Value", (int)kv.Value.Value);
                    break;
                case ArgKind.Int64:
                    writer.WriteNumber("Value", (long)kv.Value.Value);
                    break;
                default:
                    writer.WriteString("Value", kv.Value.GetFormattedValue());
                    break;
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }

    public override ArgSet? Read(ref Utf8JsonReader reader,
        Type typeToConvert, JsonSerializerOptions options)
    {
        var argSet = new ArgSet();

        static void ReadAndCheck(
            ref Utf8JsonReader reader, JsonTokenType expected)
        {
            var wasRead = reader.Read();

            if (!wasRead || reader.TokenType != expected)
                throw new JsonException();
        }

        static void ReadAndCheckProperty(
            ref Utf8JsonReader reader, string propertyName)
        {
            var wasRead = reader.Read();

            if (!wasRead || reader.TokenType != JsonTokenType.PropertyName)
                throw new JsonException();

            if (reader.GetString() != propertyName)
                throw new Exception();
        }


        static void ThrowIfNotExpected(
            ref Utf8JsonReader reader, JsonTokenType expected)
        {
            if (reader.TokenType != expected)
                throw new JsonException();
        }

        void ParseAndUpsert(ref Utf8JsonReader reader)
        {
            ThrowIfNotExpected(ref reader, JsonTokenType.PropertyName);

            var key = MultiTag.Create(reader.GetString()!);

            ReadAndCheck(ref reader, JsonTokenType.StartObject);

            ReadAndCheckProperty(ref reader, "Kind");

            reader.Read();

            var kind = reader.GetString()!.ToEnumValue<ArgKind>();

            Type type = null!;

            if (kind == ArgKind.Enum)
            {
                reader.Read();

                if (reader.GetString() != "Type")
                    throw new JsonException();

                reader.Read();

                var typeText = reader.GetString()!;

                type = Type.GetType(typeText)!;
            }

            ReadAndCheckProperty(ref reader, "Value");

            reader.Read();

            argSet.Set(key, GetArg(ref reader, kind, type));

            reader.Read();

            ThrowIfNotExpected(ref reader, JsonTokenType.EndObject);
        }

        ThrowIfNotExpected(ref reader, JsonTokenType.StartObject);

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
                break;

            ParseAndUpsert(ref reader);
        }

        if (reader.TokenType != JsonTokenType.EndObject)
            throw new JsonException();

        return argSet;
    }

    private static Arg GetArg(ref Utf8JsonReader reader, ArgKind kind, Type type)
    {
        Arg GetArg<T>(T value) => Arg.Create<T>(value, kind);

        return kind switch
        {
            ArgKind.AccountId => GetArg(AccountId.Create(reader.GetString()!)),
            ArgKind.Boolean => GetArg(reader.GetBoolean()),
            ArgKind.ActorId => GetArg(ActorId.Create(reader.GetString()!)),
            ArgKind.DateOnly => GetArg(DateOnly.Parse(reader.GetString()!)),
            ArgKind.DateTime => GetArg(DateTime.Parse(reader.GetString()!)),
            ArgKind.Double => GetArg(reader.GetDouble()),
            ArgKind.Email => GetArg(Email.Create(reader.GetString()!)),
            ArgKind.Enum => GetArg((Enum)Enum.Parse(type, reader.GetString()!)),
            ArgKind.Float => GetArg(reader.GetSingle()),
            ArgKind.Guid => GetArg(Guid.Parse(reader.GetString()!)),
            ArgKind.Int32 => GetArg(reader.GetInt32()),
            ArgKind.Int64 => GetArg(reader.GetInt64()),
            ArgKind.Delta => GetArg(Delta.Create(reader.GetString()!)),
            ArgKind.MultiTag => GetArg(MultiTag.Create(reader.GetString()!)),
            ArgKind.Phone => GetArg(Phone.Create(reader.GetString()!)),
            ArgKind.String => GetArg(reader.GetString()!),
            ArgKind.TimeOnly => GetArg(TimeOnly.Parse(reader.GetString()!)),
            ArgKind.TimeSpan => GetArg(TimeSpan.Parse(reader.GetString()!)),
            ArgKind.Tag => GetArg(Tag.Create(reader.GetString()!)),
            ArgKind.TradeDate => GetArg(KnownTradeDates.From(
                DateOnly.Parse(reader.GetString()!))),
            ArgKind.Uri => GetArg(new Uri(reader.GetString()!)),
            _ => throw new JsonException()
        };
    }
}