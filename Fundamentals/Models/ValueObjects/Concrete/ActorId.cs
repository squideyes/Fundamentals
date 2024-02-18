// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public sealed class ActorId : ValueObjectBase<ActorId>
{
    public const int Length = 8;

    public string? Value { get; private set; }

    protected override void SetProperties(string input)
    {
        Value = input;
    }

    public static bool IsInput(string input) => 
        Base32Id.IsInput(input, Length);

    public static ActorId Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out ActorId result) =>
        DoTryCreate(input, IsInput, out result);

    public static ActorId Next() => Create(Base32Id.Next(Length));
}