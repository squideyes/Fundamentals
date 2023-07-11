// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation.Results;
using LoggingDemo;
using Serilog;
using SquidEyes.Fundamentals;
using SquidEyes.Fundamentals.LoggingDemo;

// "UseSerilog" hooks Serilog into Microsoft.Extensions.Logging
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

// Typically read in from KeyVault or another secret store
var seqApiUri = new Uri("http://host.docker.internal:5341");
var seqApiKey = (string)null!;

// Zero or more app-specific fields to enrich ALL log-items with
var enrichWith = new TagValueSet()
{
    { Tag.Create("ActorId"), "ABC123" },
    { Tag.Create("JunketId"), 12345 },
    { Tag.Create("RunDate"), DateTime.Today }
};

var logFilePath = Path.Combine(Path.GetTempPath(), "LoggingDemo.log");

// Create a standard logger; in this case with a trio of app-specific
// fields to enrich ALL of the log-items with, an extra (File) sink
// and a custom ActorId transform
Log.Logger = SerilogHelper.GetStandardLogger(
    new StandardLoggerArgs()
    {
        SeqApiUri = seqApiUri,
        SeqApiKey = seqApiKey,
        MinSeverity = Severity.Debug,
        EnrichWith = new TagValueSet()
        {
            { Tag.Create("ActorId"), "ABC123" },
            { Tag.Create("JunketId"), 12345 },
            { Tag.Create("RunDate"), DateTime.Today }
        }
    },
    configure =>
    {
        configure.WriteTo.File(
            logFilePath, rollingInterval: RollingInterval.Day);

        configure.Destructure
            .ByTransforming<ActorId>(v => v.ToString());
    });

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
Console.WriteLine($"For more info, see: {seqApiUri} and {logFilePath}");