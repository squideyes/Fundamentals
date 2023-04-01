// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Diagnostics;

namespace SquidEyes.Fundamentals;

public abstract class VerbBase<T, M>
    where M : VerbBase<T, M>
{
    private readonly Func<T, bool> canEval;

    private readonly string prefix;

    internal VerbBase(T value, 
        string argName, Func<T, bool> canEval, string prefix)
    {
        Value = value;
        ArgName = argName!;
        this.canEval = canEval;
        this.prefix = prefix;
    }

    internal T Value { get; }

    protected string ArgName { get; private set; }
    protected string? Message { get; private set; }

    [DebuggerHidden]
    public M WithArgName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        ArgName = value;

        return (M)this;
    }

    [DebuggerHidden]
    public M WithMessage(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new InvalidOperationException();

        Message = value;

        return (M)this;
    }

    [DebuggerHidden]
    internal T ThrowErrorIfNotIsValid(Func<T, bool> isValid, Func<T, string> getSuffix)=>
        ThrowErrorIfNotIsValid(isValid, $"{prefix}{getSuffix(Value)}.");

    [DebuggerHidden]
    internal T ThrowErrorIfNotIsValid(Func<T, bool> isValid, string message)
    {
        if (canEval(Value) && !isValid(Value))
            throw new VerbException(ArgName, Message ?? message);

        return Value;
    }
}