﻿using ErrorOr;
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
        var uri = ConfigHelper.CreateUri(
            "SeqApiUri", config["Serilog:SeqApiUri"]!);

        var key = ConfigHelper.CreateString(
            "SeqApiKey", config["Serilog:SeqApiKey"]!,
            v => v.IsNullOrNonNullAndTrimmed());

        var ms = ConfigHelper.CreateEnum<Severity>(
            "MinSeverity", config["Serilog:MinSeverity"]!);

        if (ConfigHelper.TryGetErrors("SerilogArgs.CreateError",
            [uri, key, ms], out List<Error> errors))
        {
            return errors;
        }

        return new SerilogArgs(
            uri.Value!, key.Value.WhitespaceToNull(), ms.Value);
    }
}