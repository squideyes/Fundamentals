// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using LoggingDemo;
using Serilog;
using SquidEyes.Fundamentals;
using SquidEyes.Fundamentals.LoggingDemo;
using static SquidEyes.Fundamentals.InitLoggerBuilder;
using MEL = Microsoft.Extensions.Logging;

// Gets IConfiguration used to configure the program
var config = GetConfig(args, "LoggingDemo__");

// Used for Serilog.Log.Logger initialization
var (junketId, userId, seqApiUri, seqApiKey, logLevel) = GetInitTagArgs();

// Only used to log initialization failures
var initLogger = GetInitLogger();

// If one or more of the TagArgs are invalid, exit early
if (!initLogger.IsValid([junketId, userId, seqApiUri, seqApiKey, logLevel]))
    return ExitCode.InitFailure;

// The filename to write custom log-items to
var logFileName = Path.Combine(Path.GetTempPath(), "LoggingDemo.log");

// Instantiate and configure a new Serilog.Log.Logger
InitSerilogLogger(LogLevel.Debug, configure =>
{
    configure.Enrich.WithProperty(junketId.Tag.Value!, junketId.Arg);
    configure.Enrich.WithProperty(userId.Tag.Value!, userId.Arg);
    configure.Enrich.WithProperty("RunDate", DateOnly.FromDateTime(DateTime.Today));

    configure.Destructure.ByTransforming<Broker>(v => v.ToCode());

    //configure.WriteTo.File(logFileName, rollingInterval: RollingInterval.Day);

    var logEventLevel = logLevel.Arg.ToLogEventLevel();
});

// Get a Microsoft.Extensions.Logging.ILogger 
var logger = GetLogger();

// "UseSerilog" hooks Serilog into Microsoft.Extensions.Logging DI
var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddHostedService<Worker>();
    })
    .UseSerilog()
    .Build();

// Log a custom log-item
logger.LogLogonSucceeded("LogonTest", Broker.DiscountTrading, Gateway.UsWest);

// Simple ad-hoc log-items can be intermingled with standard log-items
logger.LogDebug("Simple ad-hoc log item");

// Complex ad-hoc log-items can be intermingled with standard log-items
logger.LogInformation("Ad-Hoc Info={@Info}", new { Code = "ABC123", Message = "It works!" });

// Log an miscellaneous TagArgSet
logger.LogMiscTagArgs("TagArgSetTest", "TestTagArgsSet", GetTagArgs(), MiscLogLevel.Warning);

// Log a miscellaneous warning message 
logger.LogMiscMessage(
    "MiscEventTest1", "Oopsie", "Something Went Wrong!", MiscLogLevel.Warning);

// Log a miscellaneous information message
logger.LogMiscMessage("MiscEventTest2", "OhGoodie", "Something Went Right!");

// Create a default Person
var person = new Person();

// Create a Person validator
var validator = new Person.Validator();

// Should log a ValidationFailure
logger.LogIfValidationFailure("ValidationTest", validator.Validate(person));

// Update Person properties with valid values
person.FirstName = "Some";
person.LastName = "Dude";

// If ValidationResult.IsValid is true, don't write out log-item 
logger.LogIfValidationFailure("ValidationTest", validator.Validate(person));

// Run the Worker

// Always close and flush before terminating your app
Log.CloseAndFlush();

// Program ends
Console.WriteLine();
Console.WriteLine($"For info, see: {config["Serilog:SeqApiUri"]} or {logFileName}");

return ExitCode.Success;

// Loads (but doesn't validate!) configuration values
static IConfiguration GetConfig(string[] args, string envVarPrefix)
{
    var mappings = new Dictionary<string, string>()
    {
        { "--JunketId", "Context:JunketId" },
        { "--UserId", "Context:UserId" },
        { "--SeqApiUri", "Serilog:SeqApiUri" },
        { "--SeqApiKey", "Serilog:SeqApiKey" },
        { "--LogLevel", "Serilog:LogLevel" }
    };

    return new ConfigurationBuilder()
        .AddEnvironmentVariables(envVarPrefix)
        .AddCommandLine(args, mappings)
        .Build();
}

// Gets a Microsoft.Extensions.Logging.ILogger
static MEL.ILogger GetLogger()
{
    using var loggerFactory = LoggerFactory.Create(
        builder => { builder.AddSerilog(); });

    return loggerFactory.CreateLogger<Program>();
}

// Gets TagArgs used for logger initialization
InitTagArgs GetInitTagArgs()
{
    var junketId = config["Context:JunketId"]!.ToInt32TagArg("JunketId", v => v > 0);
    var userId = config["Context:UserId"]!.ToTextLineTagArg("UserId", true);
    var seqApiUri = config["Serilog:SeqApiUri"]!.ToUriTagArg("SeqApiUri");
    var seqApiKey = config["Serilog:SeqApiKey"]!.ToTextLineTagArg("SeqApiKey", false);
    var logLevel = config["Serilog:LogLevel"]!.ToEnumTagArg<LogLevel>("LogLevel");

    return new InitTagArgs(junketId, userId, seqApiUri, seqApiKey, logLevel);
}

static TagArgSet GetTagArgs()
{
    return new TagArgSet()
    {
        TagArg<bool>.Create("Bool", true),
        TagArg<byte>.Create("Byte", byte.MaxValue),
        TagArg<char>.Create("Char", 'Z'),
        TagArg<DateOnly>.Create("DateOnly", DateOnly.MaxValue),
        TagArg<DateTime>.Create("DateTime", DateTime.MaxValue),
        TagArg<double>.Create("Double", double.MaxValue),
        TagArg<Email>.Create("Email", Email.Create("somedude@someco.com")),
        TagArg<float>.Create("Float", float.MaxValue),
        TagArg<Guid>.Create("Guid", Guid.NewGuid()),
        TagArg<short>.Create("Short", short.MaxValue),
        TagArg<int>.Create("Int32", int.MaxValue),
        TagArg<long>.Create("Int64", long.MaxValue),
        TagArg<MultiTag>.Create("MultiTag", MultiTag.Create("A:B:C")),
        TagArg<Phone>.Create("Phone", Phone.Create("+1 212-333-4444")),
        TagArg<Tag>.Create("Tag", Tag.Create("A")),
        TagArg<TimeOnly>.Create("TimeOnly", TimeOnly.MaxValue),
        TagArg<TimeSpan>.Create("TimeSpan", TimeSpan.MaxValue),
        TagArg<Uri>.Create("Uri", new Uri("https://cnn.com"))
    };
}

// Properties loaded by GetInitTagArgs()
record InitTagArgs(TagArg<int> JunketId, TagArg<string> UserId,
    TagArg<Uri> SeqApiUri, TagArg<string> SeqApiKey, TagArg<LogLevel> LogLevel);