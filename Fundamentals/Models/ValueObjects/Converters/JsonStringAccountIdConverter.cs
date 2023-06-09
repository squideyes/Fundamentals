﻿// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class JsonStringAccountIdConverter : JsonConverter<AccountId>
{
    public override AccountId Read(ref Utf8JsonReader reader, 
        Type typeToConvert, JsonSerializerOptions options)
    {
        return AccountId.Create(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, 
        AccountId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}