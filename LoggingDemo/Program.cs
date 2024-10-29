// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using LoggingDemo;
using Serilog;
using Serilog.Sinks.OpenTelemetry;
using SquidEyes.Fundamentals;
using System.Net;
using System.Text.Json;
using MEL = Microsoft.Extensions.Logging;

// Try to load and validate InitValues
if (!TryGetInitValues(args, out var initValues))
    return ExitCode.InitFailure;

// Create a configured logger
var logger = GetLogger(initValues);

// Log a custom log-item
logger.LogLogonSucceeded(Broker.DiscountTrading, Gateway.UsWest, "ABC123");

// Log a custom log-item
logger.LogLogonFailed(Broker.AmpFutures, Gateway.UsEast, "XYZ987", 
    HttpStatusCode.Forbidden, "Forbidden");

// Simple ad-hoc log-items can be intermingled with standard log-items
logger.LogDebug("Simple ad-hoc log item");

// Complex ad-hoc log-items can be intermingled with standard log-items
logger.LogInformation("Ad-Hoc Info={@Info}", 
    new { Code = "ABC123", Message = "It works!" });

// Log a miscellaneous TagArgSet
logger.LogMiscTagArgs("OhGoodie", DemoHelper.GetTagArgs());

// Log a miscellaneous information message
logger.LogMiscInfo("OhGoodie", "Something Went Right!");

// Log a Result Failure
logger.LogResultFailure("LogFailureTest", GetFailure());

// Initializes a FluentValidation validator for Person
var personValidator = new Person.Validator();

// Does not emit a log-item since Person is valid
logger.LogIfValidationFailure(
    personValidator.Validate(new Person("Some", null, "Dude")));

// Does not emit a log-item since Person is valid
logger.LogIfValidationFailure(
    personValidator.Validate(new Person("Some", 'T', "Dude")));

// Emits one log-item because Initial is invalid 
logger.LogIfValidationFailure(
    personValidator.Validate(new Person("Some", '1', "Dude")));

// Emits two log-items because FirstName and LastName are null
logger.LogIfValidationFailure(
    personValidator.Validate(new Person(null!, null, null!)));

// Emits a RawTx with a JSON payload
logger.LogRawTxReceived(GetRawTxPayload());

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

    var seqApiUri = config["Serilog:SeqApiUri"]!.ToUriTagArg("SeqApiUri");
    var seqApiKey = config["Serilog:SeqApiKey"]!.ToTextLineTagArg("SeqApiKey", false);
    var logLevel = config["Serilog:LogLevel"]!.ToEnumTagArg<LogLevel>("LogLevel");

    if (!initLogger.IsValid([seqApiUri, seqApiKey, logLevel]))
        return false;

    var logFileName = Path.Combine(Path.GetTempPath(), "LoggingDemo.log");

    initValues = new InitValues(seqApiUri.Arg, seqApiKey.Arg, logLevel.Arg, logFileName);

    return true;
}

// Gets an initialized MEL.ILogger
static MEL.ILogger GetLogger(InitValues initValues)
{
    ValidatorOptions.Global.DisplayNameResolver = (_, m, _) => m?.Name;

    SerilogHelper.InitLogDotLogger(LogLevel.Debug, configure =>
    {
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

// Get a Failure with multipl errors
static Result<string> GetFailure()
{
    var error1 = new Error("FailureTest:BadCode", "The Code is bad!");

    var error2 = new Error("FailureTest:BadName", "The Name is invalid",
    [
        "true".ToBoolTagArg("AllowSpaces"),
        "SomeDude".ToTextLineTagArg("RequiredPrefix", true)
    ]);

    return Result.Failure<string>([error1, error2]);
}

// Gets a comprehensive RawTx
static JsonElement GetRawTxPayload()
{
    const string JSON = """
        {
            "StringValue": "Hello, World!",
            "NumberInteger": 42,
            "NumberFloat": 3.14159,
            "NumberLarge": 9223372036854775807,
            "BooleanTrue": true,
            "BooleanFalse": false,
            "NullValue": null,
            "DateValue": "2024-03-15T14:30:00Z",
            "ArrayEmpty": [],
            "ArraySimple": [1, 2, 3, 4, 5],
            "ArrayMixed": [
                "string",
                42,
                true,
                null,
                3.14
            ],
            "NestedObject": {
                "Name": "John Doe",
                "Age": 30,
                "Address": {
                    "Street": "123 Main St",
                    "City": "Springfield",
                    "ZipCode": "12345"
                },
                "Contacts": [
                    {
                        "Type": "Email",
                        "Value": "john@example.com"
                    },
                    {
                        "Type": "Phone",
                        "Value": "+1-555-123-4567"
                    }
                ]
            },
            "Metadata": {
                "Tags": ["test", "sample", "json"],
                "Version": 1.0,
                "Active": true,
                "LastUpdated": "2024-03-15T14:30:00Z"
            }
        }
        """;

    var doc = JsonDocument.Parse(JSON);

    return doc.RootElement;
}

// Properties loaded by GetInitTagArgs()
record InitValues(
    Uri SeqApiUri, 
    string SeqApiKey, 
    LogLevel LogLevel, 
    string LogFileName);