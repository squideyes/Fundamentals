// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Text.Json;
using System.Text.Json.Serialization;

namespace SquidEyes.Fundamentals;

public class ExceptionCaught : LogItemBase
{
    private static readonly JsonSerializerOptions jso = GetJsonSerializerOptions();

    private readonly ExceptionInfo exceptionInfo;

    public ExceptionCaught(Tag activity, Exception error, TagValueSet? metadata = null!)
        : base(Severity.Error, activity, metadata)
    {
        error.MayNotBe().Null();
        metadata = metadata?.MustBe().True(v => !v!.IsEmpty);

        exceptionInfo = new ExceptionInfo(error);
    }

    protected override TagValueSet GetCustomTagValues()
    {
        var tagValues = new TagValueSet();

        tagValues.Upsert("ErrorType", exceptionInfo.ErrorType);
        tagValues.Upsert("Message", exceptionInfo.Message);
        tagValues.Upsert("Exception", 
            JsonSerializer.Serialize(exceptionInfo.Message, jso));

        return tagValues;
    }

    private static JsonSerializerOptions GetJsonSerializerOptions()
    {
        var jso = new JsonSerializerOptions { WriteIndented = false };

        jso.Converters.Add(new JsonStringEnumConverter());

        return jso;
    }
}