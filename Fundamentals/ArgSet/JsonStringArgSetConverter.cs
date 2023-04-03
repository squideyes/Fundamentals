// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Collections.Concurrent;
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

        static void ThrowIfNotExpected(
            ref Utf8JsonReader reader, JsonTokenType expected) 
        {
            if (reader.TokenType != expected)
                throw new JsonException();
        }

        static Arg GetArg(ref Utf8JsonReader reader)
        {
            ReadAndCheck(ref reader, JsonTokenType.StartObject);

            ArgKind? kind = null;
            Type type = null!;
            object value = null!;

            void SetProperty(ref Utf8JsonReader reader)
            {
                var name = reader.GetString();

                switch (name)
                {
                    case "Kind":
                        reader.Read();
                        kind = reader.GetString()!.ToEnumValue<ArgKind>();
                        break;
                    case "Type":
                        reader.Read();
                        type = Type.GetType(reader.GetString()!)!;
                        break;
                    case "Value":
                        reader.Read();
                        break;
                }
            }

            while (reader.TokenType != JsonTokenType.EndObject)
            {
                reader.Read();

                switch(reader.TokenType)
                {
                    case JsonTokenType.PropertyName:
                        SetProperty(ref reader);
                        break;
                }
            }

            ThrowIfNotExpected(ref reader, JsonTokenType.EndObject);

            return null;
        }

        ThrowIfNotExpected(ref reader, JsonTokenType.StartObject);

        while (reader.Read())
        {
            ThrowIfNotExpected(ref reader, JsonTokenType.PropertyName);

            var name = reader.GetString();

            var arg = GetArg(ref reader);
        }

        ReadAndCheck(ref reader, JsonTokenType.EndObject);

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