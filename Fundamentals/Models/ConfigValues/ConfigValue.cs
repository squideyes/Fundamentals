// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigValue<T>
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
                $"The '{tag}' value may not be NULL",
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
}