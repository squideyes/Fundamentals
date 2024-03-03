// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using ErrorOr;
using FluentValidation.Results;

namespace SquidEyes.Fundamentals;

public abstract class ConfigItemBase : IConfigItem
{
    public Tag Tag { get; }
    public ConfigStatus Status { get; }
    public string? Message { get; }

    public bool IsValid => Status == ConfigStatus.IsValid;

    protected ConfigItemBase(Tag tag)
    {
        Tag = tag;
        Status = ConfigStatus.IsValid;
        Message = null;
    }

    protected ConfigItemBase(Tag tag, string input, ConfigStatus status)
    {
        Tag = tag;

        Status = status;

        Message = status switch
        {
            ConfigStatus.NullOrEmpty =>
                $"The '{tag}' input may not be null or empty",
            ConfigStatus.ParseError =>
                $"The '{tag}' input could not be parsed (Input: {input})",
            ConfigStatus.NotValid =>
                $"The '{tag}' value is an invalid string.",
            _ =>
                throw new ArgumentOutOfRangeException(nameof(status))
        };
    }

    public Error ToError(string code = null!)
    {
        if (Status == ConfigStatus.IsValid)
            throw new InvalidOperationException("IsValid == false!");

        return Error.Validation(code, Message!);
    }

    public ValidationFailure ToValidationFailure() =>
        new(Tag.ToString(), Message);

    protected void ThrowWhenNotIsValid()
    {
        if (Status != ConfigStatus.IsValid)
        {
            throw new InvalidOperationException(
                "A non-valid value may not be returned!");
        }
    }
}