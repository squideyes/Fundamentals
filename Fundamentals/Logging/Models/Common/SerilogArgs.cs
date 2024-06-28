// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Microsoft.Extensions.Configuration;
using SquidEyes.Fundamentals.Results;

namespace SquidEyes.Fundamentals;

public class SerilogArgs
{
    private SerilogArgs(Uri seqApiUri, string seqApiKey, Severity minSeverity)
    {
        SeqApiUri = seqApiUri;
        SeqApiKey = seqApiKey;
        MinSeverity = minSeverity;
    }

    public Uri SeqApiUri { get; }
    public string SeqApiKey { get; }
    public Severity MinSeverity { get; }

    public static Result<SerilogArgs> Create(IConfiguration config)
    {
        const string ERROR_CODE = "SerilogArgsCreateError";

        var uri = config["Serilog:SeqApiUri"]!.ToUriArg("SeqApiUri");

        if (!uri.IsValid)
            return uri.ToFailureResult<SerilogArgs>(ERROR_CODE);

        var key = config["Serilog:SeqApiKey"]!.ToStringArg(
            "SeqApiKey", true, v => v.IsNullOrNonNullAndTrimmed());

        if (!key.IsValid)
            return key.ToFailureResult<SerilogArgs>(ERROR_CODE);

        var ms = config["Serilog:MinSeverity"]!.ToEnumArg<Severity>("MinSeverity");

        if (!ms.IsValid)
            return uri.ToFailureResult<SerilogArgs>(ERROR_CODE);

        return new SerilogArgs(uri.Value!, key.Value.WhitespaceToNull(), ms.Value!);
    }
}