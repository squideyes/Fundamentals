// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using Microsoft.Extensions.Configuration;

namespace SquidEyes.Fundamentals;

public class SerilogArgs
{
    private SerilogArgs(
        Uri seqApiUri, string seqApiKey, Severity minSeverity)
    {
        SeqApiUri = seqApiUri;
        SeqApiKey = seqApiKey;
        MinSeverity = minSeverity;
    }

    public Uri SeqApiUri { get; }
    public string SeqApiKey { get; }
    public Severity MinSeverity { get; }

    public static ErrorOr<SerilogArgs> Create(IConfiguration config)
    {
        //var uri = config["Serilog:SeqApiUri"]!
        //    .ToSoftUri("SeqApiUri", UriKind.Absolute);

        //var key = config["Serilog:SeqApiKey"]!.ToSoftString(
        //    "SeqApiKey", v => v.IsNullOrNonNullAndTrimmed());

        //var ms = config["Serilog:MinSeverity"]!
        //    .ToSoftEnum<Severity>("MinSeverity");

        //if (SoftValueExtenders.TryGetErrors("SerilogArgs.CreateError",
        //    [uri, key, ms], out List<Error> errors))
        //{
        //    return errors;
        //}

        //return new SerilogArgs(
        //    uri.Value!, key.Value.WhitespaceToNull(), ms.Value);

        return default;
    }
}