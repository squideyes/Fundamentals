﻿// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringEmailConverter : JsonConverter<Email>
{
    public override Email Read(ref Utf8JsonReader reader, 
        Type typeToConvert, JsonSerializerOptions options)
    {
        return Email.Create(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, 
        Email value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}