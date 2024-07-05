// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringTagArgSetConverter : JsonConverter<TagArgSet>
{
    public override TagArgSet? Read(
        ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(
        Utf8JsonWriter writer, TagArgSet value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        foreach (var tagArg in value)
        {
            writer.WritePropertyName(tagArg.Tag.Value!);

            writer.WriteStartObject();

            writer.WriteString("Kind", tagArg.Kind.ToString());

            if (tagArg.Kind == TagArgArgKind.Enum)
                writer.WriteString("Type", tagArg.TypeName);
            else if (tagArg.Kind == TagArgArgKind.TextLine)
                writer.WriteString("Filter", tagArg.Filter.ToString());

            switch (tagArg.Kind)
            {
                case TagArgArgKind.Bool:
                    writer.WriteBoolean("Value", tagArg.GetArgAs<bool>());
                    break;
                case TagArgArgKind.Byte:
                    writer.WriteNumber("Value", tagArg.GetArgAs<byte>());
                    break;
                case TagArgArgKind.Double:
                    writer.WriteNumber("Value", tagArg.GetArgAs<double>());
                    break;
                case TagArgArgKind.Float:
                    writer.WriteNumber("Value", tagArg.GetArgAs<float>());
                    break;
                case TagArgArgKind.Int16:
                    writer.WriteNumber("Value", tagArg.GetArgAs<short>());
                    break;
                case TagArgArgKind.Int32:
                    writer.WriteNumber("Value", tagArg.GetArgAs<int>());
                    break;
                case TagArgArgKind.Int64:
                    writer.WriteNumber("Value", tagArg.GetArgAs<long>());
                    break;
                default:
                    writer.WriteString("Value", tagArg.ToString());
                    break;
            }

            writer.WriteEndObject();
        }

        writer.WriteEndObject();
    }
}