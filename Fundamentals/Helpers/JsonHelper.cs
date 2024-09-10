using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public static class JsonHelper
{
    public static JsonSerializerOptions GetJsonSerializerOptions(
        bool indented = false, Action<JsonSerializerOptions> configure = null!)
    {
        var options = new JsonSerializerOptions()
        {
            AllowTrailingCommas = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = indented
        };

        options.Converters.Add(new JsonStringEnumConverter());

        if (configure is not null)
            configure(options);

        return options;
    }
}
