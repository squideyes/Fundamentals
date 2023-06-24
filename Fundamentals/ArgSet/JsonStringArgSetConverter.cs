// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringArgSetConverter : JsonConverter<ArgSet>
{
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

            switch (kind)
            {
                case ArgKind.AccountId:
                    argSet.Upsert(key, AccountId.Create(reader.GetString()!));
                    break;
                case ArgKind.Boolean:
                    argSet.Upsert(key, reader.GetBoolean());
                    break;
                case ArgKind.ClientId:
                    argSet.Upsert(key, ClientId.Create(reader.GetString()!));
                    break;
                case ArgKind.DateOnly:
                    argSet.Upsert(key, DateOnly.Parse(reader.GetString()!));
                    break;
                case ArgKind.DateTime:
                    argSet.Upsert(key, DateTime.Parse(reader.GetString()!));
                    break;
                case ArgKind.Double:
                    argSet.Upsert(key, reader.GetDouble());
                    break;
                case ArgKind.Email:
                    argSet.Upsert(key, Email.Create(reader.GetString()!));
                    break;
                case ArgKind.Enum:
                    argSet.UpsertEnum(
                        key, Enum.Parse(type, reader.GetString()!));
                    break;
                case ArgKind.Float:
                    argSet.Upsert(key, reader.GetSingle());
                    break;
                case ArgKind.Guid:
                    argSet.Upsert(key, Guid.Parse(reader.GetString()!));
                    break;
                case ArgKind.Int32:
                    argSet.Upsert(key, reader.GetInt32());
                    break;
                case ArgKind.Int64:
                    argSet.Upsert(key, reader.GetInt64());
                    break;
                case ArgKind.Delta:
                    argSet.Upsert(key, Delta.Create(reader.GetString()!));
                    break;
                case ArgKind.Phone:
                    argSet.Upsert(key, Phone.Create(reader.GetString()!));
                    break;
                case ArgKind.String:
                    argSet.Upsert(key, reader.GetString()!);
                    break;
                case ArgKind.TimeOnly:
                    argSet.Upsert(key, TimeOnly.Parse(reader.GetString()!));
                    break;
                case ArgKind.TimeSpan:
                    argSet.Upsert(key, TimeSpan.Parse(reader.GetString()!));
                    break;
                case ArgKind.Tag:
                    argSet.Upsert(key, Tag.Create(reader.GetString()!));
                    break;
                case ArgKind.Uri:
                    argSet.Upsert(key, new Uri(reader.GetString()!));
                    break;
                default:
                    throw new JsonException();
            };

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

    public override void Write(Utf8JsonWriter writer,
        ArgSet value, JsonSerializerOptions options)
    {
        static string GetValueString(Arg arg)
        {
            return arg.Kind switch
            {
                ArgKind.DateOnly => arg.Get<DateOnly>()
                    .ToString("MM/dd/yyyy"),
                ArgKind.DateTime => arg.Get<DateTime>()
                    .ToString("MM/dd/yyyy HH:mm:ss.fff"),
                ArgKind.TimeSpan => arg.Get<TimeSpan>()
                    .ToString(@"d\.hh\:mm\:ss\.fff"),
                ArgKind.TimeOnly => arg.Get<TimeOnly>()
                    .ToString("HH:mm:ss.fff"),
                _ => arg.Value.ToString()!
            };
        }

        writer.WriteStartObject();

        foreach (var kv in value)
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
                    writer.WriteString("Value", GetValueString(kv.Value));
                    break;
            }


            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }
}