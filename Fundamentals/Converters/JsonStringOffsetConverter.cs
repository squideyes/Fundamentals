// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringOffsetConverter : JsonConverter<Offset>
{
    public override Offset Read(ref Utf8JsonReader reader, 
        Type typeToConvert, JsonSerializerOptions options)
    {
        return Offset.From(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer,
        Offset value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}