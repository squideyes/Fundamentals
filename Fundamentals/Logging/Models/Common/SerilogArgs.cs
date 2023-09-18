using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using OneOf;

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

    public static OneOf<SerilogArgs, ValidationResult> Create(IConfiguration config)
    {
        var validationResult = new ValidationResult();

        var uri = ConfigHelper.CreateUri(
            "SeqApiUri", config["Serilog:SeqApiUri"]!).Validate(validationResult);

        var key = ConfigHelper.CreateString("SeqApiKey", 
            config["Serilog:SeqApiKey"]!, v => string.IsNullOrWhiteSpace(v) 
                || v.IsNonNullAndTrimmed()).Validate(validationResult);

        var ms = ConfigHelper.CreateEnum<Severity>(
            "MinSeverity", config["Serilog:MinSeverity"]!).Validate(validationResult);

        if (!validationResult.IsValid)
            return validationResult;

        return new SerilogArgs(uri.Value!, key.Value.WhitespaceToNull(), ms.Value);
    }
}