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
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, ArgSet value, 
        JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}