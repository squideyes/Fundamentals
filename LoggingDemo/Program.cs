// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;
using LoggingDemo;
using Serilog;
using SquidEyes.Fundamentals;
using SquidEyes.Fundamentals.LoggingDemo;
using System.Diagnostics;
using static SquidEyes.Fundamentals.StandardLoggerBuilder;

// Use SelfLog to catch internal Serilog errors
Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));
Serilog.Debugging.SelfLog.Enable(Console.Error);

// The filename to write custom log-items to
var logFileName = Path.Combine(Path.GetTempPath(), "LoggingDemo.log");

// Gets IConfiguration used by the program
var config = GetConfig(args, "LoggingDemo__");

// Only used to log boostrap failures
var logger = GetBootstrapLogger();

// Gets enrichment data to include with EVERY log-item
if (!TryGetEnrichWiths(logger, config, out TagValueSet enrichWiths))
    return;

// Try to instantiate a new "standard" Serilog.Log.Logger
if (!TrySetStandardLogger(logger, config, enrichWiths, Customize))
    return;

// "UseSerilog" hooks Serilog into Microsoft.Extensions.Logging
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

// Log "LogonSuccess" (a custom log-item)
Log.Logger.Log(new LogonSucess(Brokerage.CanonTrading,
    new[] { Gateway.Chicago, Gateway.UsWest }));

// Log "MiscLogItem" with a simple "Message" Context
Log.Logger.Log(new MiscLogItem(Severity.Warn,
    Tag.Create("MiscWarning"), TagValueSet.From("Up != Down")));

// Free-form messages can be intermingled with LogItems
Log.Logger.Debug("Who let the dogs out?");

// Log "MiscLogItem" with a multiple tag-value Context
Log.Logger.Log(new MiscLogItem(
    Severity.Debug, Tag.Create("GotSettings"),
    new TagValueSet()
    {
        { Tag.Create("Border"), 2 },
        { Tag.Create("Background"), ConsoleColor.Yellow },
        { Tag.Create("Foreground"), ConsoleColor.Black },
        { Tag.Create("ShowHelp"), true },
        { Tag.Create("Session"), new DateOnly(2023, 1, 1) }
    }));

// Log individual FluentValidation ValidationFailures
Log.Logger.Log(new ValidationFailed(Tag.Create("StartupValidation"),
    new ValidationFailure("Id", "'{Id}' must be a valid Id.")));

await host.RunAsync();

// Always close and flush before terminating your app
Log.CloseAndFlush();

Console.WriteLine();
Console.WriteLine($"For more info, see: {config["Serilog:SeqApiUri"]} or {logFileName}");

// Loads (but doesn't validate!) configuration values
static IConfiguration GetConfig(string[] args, string envVarPrefix)
{
    var mappings = new Dictionary<string, string>()
    {
        { "--JunketId", "Context:JunketId" },
        { "--UserId", "Context:UserId" },
        { "--SeqApiUri", "Serilog:SeqApiUri" },
        { "--SeqApiKey", "Serilog:SeqApiKey" },
        { "--MinSeverity", "Serilog:MinSeverity" }
    };

    return new ConfigurationBuilder()
        .AddEnvironmentVariables(envVarPrefix)
        .AddCommandLine(args, mappings)
        .Build();
}

// Customizes the standard logger
void Customize(LoggerConfiguration config)
{
    config.WriteTo.File(logFileName, rollingInterval: RollingInterval.Day);

    config.Destructure.ByTransforming<Brokerage>(v =>
    {
        return v switch
        {
            Brokerage.CanonTrading => "Canon Trading",
            Brokerage.DiscountTrading => "Discount Trading",
            Brokerage.AmpFutures => "Amp Trading",
            Brokerage.OptimusFutures => "Optimus Futures",
            _ => throw new ArgumentOutOfRangeException(nameof(v))
        };
    });
}

// Tries to get a TagValueSet to enrich log-items with
static bool TryGetEnrichWiths(Serilog.ILogger logger,
    IConfiguration config, out TagValueSet enrichWiths)
{
    var validationResult = new ValidationResult();

    var junketId = ConfigHelper.CreateInt32("JunketId",
        config["Context:JunketId"]!, v => v > 0).Validate(validationResult);

    var userId = ConfigHelper.CreateString("UserId", config["Context:UserId"]!,
        v => v.IsNonNullAndTrimmed()).Validate(validationResult);

    if (!validationResult.IsValid)
    {
        enrichWiths = null!;

        foreach (var e in validationResult.Errors)
            logger.Warning($"{e.PropertyName}: {e.ErrorMessage}");

        return false;
    }

    enrichWiths = new TagValueSet
    {
        { junketId.Tag, junketId.Value },
        { userId.Tag, userId.Value! },
        { "RunDate", DateOnly.FromDateTime(DateTime.Today) }
    };

    return true;
}