// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public class ConfigValue<T> : IConfigValue
{
    internal ConfigValue(Tag tag, T value)
    {
        Tag = tag;
        Status = ConfigValueStatus.IsValid;
        Value = value;
    }

    internal ConfigValue(Tag tag, string input, ConfigValueStatus status)
    {
        Tag = tag;
        Status = status;

        Message = status switch
        {
            ConfigValueStatus.NullInput =>
                $"The '{tag}' value may not be null",
            ConfigValueStatus.BadInput =>
                $"The '{tag}' value may not be whitespace, empty or contain non-ascii characters",
            ConfigValueStatus.ParseError =>
                $"The '{tag}' value could not be parsed (Input: {input})",
            ConfigValueStatus.NotValid =>
                $"The '{tag}' value is an invalid {typeof(T).Name}.",
            _ => null
        };
    }

    public Tag Tag { get; internal set; }
    public ConfigValueStatus Status { get; internal set; }
    public T? Value { get; internal set; }
    public string? Message { get; internal set; }

    public bool IsValid => Status == ConfigValueStatus.IsValid;

    public ValidationFailure ToValidationFailure() =>
        new(Tag.ToString(), Message);

    public Error ToError(string code = null!)
    {
        if (Status == ConfigValueStatus.IsValid)
            throw new InvalidOperationException();

        code.MustBe().True(v => v is null || v.IsNonNullAndTrimmed());

        return Error.Validation(code, Tag + ": " + Message!);
    }
}