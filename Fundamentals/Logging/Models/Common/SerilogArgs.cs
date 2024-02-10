using ErrorOr;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;

using EO = ErrorOr;

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

    public static ErrorOr<SerilogArgs> Create(IConfiguration config)
    {
        var result = new ValidationResult();

        var uri = ConfigHelper.CreateUri(
            "SeqApiUri", config["Serilog:SeqApiUri"]!).Validate(result);

        var key = ConfigHelper.CreateString("SeqApiKey",
            config["Serilog:SeqApiKey"]!, v => string.IsNullOrWhiteSpace(v)
                || v.IsNonNullAndTrimmed()).Validate(result);

        var ms = ConfigHelper.CreateEnum<Severity>(
            "MinSeverity", config["Serilog:MinSeverity"]!).Validate(result);

        if (!result.IsValid)
        {
            return result.Errors.ConvertAll(
                e => EO.Error.Validation(e.PropertyName, e.ErrorMessage));
        }

        return new SerilogArgs(
            uri.Value!, key.Value.WhitespaceToNull(), ms.Value);
    }
}