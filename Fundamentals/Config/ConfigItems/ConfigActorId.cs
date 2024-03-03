// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public class ConfigActorId : ConfigItemBase
{
    public ConfigActorId(Tag tag, ActorId value)
        : base(tag)
    {
        Value = value;
    }

    public ConfigActorId(Tag tag, string input, ConfigStatus status)
        : base(tag, input, status)
    {
    }

    public ActorId Value { get; } = null!;
}