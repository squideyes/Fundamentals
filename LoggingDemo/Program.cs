// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using LoggingDemo;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using SquidEyes.Fundamentals;
using SquidEyes.Fundamentals.LoggingDemo;
using MEL = Microsoft.Extensions.Logging;

// Try to load and validate InitValues
if (!TryGetInitValues(args, out var initValues))
    return ExitCode.InitFailure;

// Create a configured logger
var logger = GetLogger(initValues);

// Log a custom log-item
logger.LogLogonSucceeded("LogLogonSucceededDemo", 
    Broker.DiscountTrading, Gateway.UsWest);

// Simple ad-hoc log-items can be intermingled with standard log-items
logger.LogDebug("Simple ad-hoc log item");

// Complex ad-hoc log-items can be intermingled with standard log-items
logger.LogInformation("Ad-Hoc Info={@Info}", 
    new { Code = "ABC123", Message = "It works!" });

// Log a miscellaneous TagArgSet
logger.LogMiscTagArgs(
    "LogMiscTagArgsDemo", "OhGoodie", DemoHelper.GetTagArgs());

// Log a miscellaneous warning message 
logger.LogMiscMessage("LogMiscMessageDemo1", 
    "Oopsie", "Something Went Wrong!", MiscLogLevel.Warning);

// Log a miscellaneous information message
logger.LogMiscMessage(
    "LogMiscMessageDemo2", "OhGoodie", "Something Went Right!");

// Initializes a FluentValidation validator for Person
var personValidator = new Person.Validator();

// Does not emit a log-item since Person is valid
logger.LogIfValidationFailure("LogIfValidationFailureDemo1",
    personValidator.Validate(new Person("Some", null, "Dude")));

// Does not emit a log-item since Person is valid
logger.LogIfValidationFailure("LogIfValidationFailureDemo2",
    personValidator.Validate(new Person("Some", 'T', "Dude")));

// Emits one log-item because Initial is invalid 
logger.LogIfValidationFailure("LogIfValidationFailureDemo3",
    personValidator.Validate(new Person("Some", '1', "Dude")));

// Emits two log-items because FirstName and LastName are null
logger.LogIfValidationFailure("LogIfValidationFailureDemo4",
    personValidator.Validate(new Person(null!, null, null!)));

// "UseSerilog" hooks Serilog into Microsoft.Extensions.Logging
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

// Run the Worker
host.Run();

// Always close and flush before terminating your app
Log.CloseAndFlush();

return ExitCode.Success;

// Attempts to Load and validate InitValues
static bool TryGetInitValues(string[] args, out InitValues initValues)
{
    const string ENV_VAR_PREFIX = "LoggingDemo__";

    var mappings = new Dictionary<string, string>()
    {
        { "--JunketId", "Context:JunketId" },
        { "--UserId", "Context:UserId" },
        { "--SeqApiUri", "Serilog:SeqApiUri" },
        { "--SeqApiKey", "Serilog:SeqApiKey" },
        { "--LogLevel", "Serilog:LogLevel" }
    };

    var initLogger = SerilogHelper.GetInitLogger();

    initValues = null!;

    var config =  new ConfigurationBuilder()
        .AddEnvironmentVariables(ENV_VAR_PREFIX)
        .AddCommandLine(args, mappings)
        .Build();

    var junketId = config["Context:JunketId"]!.ToInt32TagArg("JunketId", v => v > 0);
    var userId = config["Context:UserId"]!.ToTextLineTagArg("UserId", true);
    var seqApiUri = config["Serilog:SeqApiUri"]!.ToUriTagArg("SeqApiUri");
    var seqApiKey = config["Serilog:SeqApiKey"]!.ToTextLineTagArg("SeqApiKey", false);
    var logLevel = config["Serilog:LogLevel"]!.ToEnumTagArg<LogLevel>("LogLevel");

    if (!initLogger.IsValid([junketId, userId, seqApiUri, seqApiKey, logLevel]))
        return false;

    var logFileName = Path.Combine(Path.GetTempPath(), "LoggingDemo.log");

    initValues = new InitValues(junketId.Arg, userId.Arg, 
        seqApiUri.Arg, seqApiKey.Arg, logLevel.Arg, logFileName);

    return true;
}

static MEL.ILogger GetLogger(InitValues initValues)
{
    ValidatorOptions.Global.DisplayNameResolver = (_, m, _) => m?.Name;

    SerilogHelper.InitLogDotLogger(LogLevel.Debug, configure =>
    {
        configure.Enrich.WithProperty("JunketId", initValues.JunketId);
     
        configure.Enrich.WithProperty("UserId", initValues.UserId);
        
        configure.Enrich.WithProperty(
            "RunDate", DateOnly.FromDateTime(DateTime.Today));

        configure.Destructure.ByTransforming<Broker>(v => v.ToCode());

        configure.WriteTo.File(
            initValues.LogFileName, rollingInterval: RollingInterval.Day);

        configure.WriteTo.OpenTelemetry(x =>
        {
            x.Endpoint = initValues.SeqApiUri.AbsoluteUri;
            x.Protocol = OtlpProtocol.HttpProtobuf;
            x.Headers = new Dictionary<string, string>
            {
                ["X-Seq-ApiKey"] = initValues.SeqApiKey
            };
            x.ResourceAttributes = new Dictionary<string, object>
            {
                ["service.name"] = "LoggingDemo"
            };
        });
    });

    using var loggerFactory = LoggerFactory.Create(
         builder => { builder.AddSerilog(); });

    return loggerFactory.CreateLogger<Program>();
}

// Properties loaded by GetInitTagArgs()
record InitValues(int JunketId, string UserId, Uri SeqApiUri, 
    string SeqApiKey, LogLevel LogLevel, string LogFileName);