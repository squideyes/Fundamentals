namespace SquidEyes.Fundamentals;

public sealed class AccountId : ValueObjectBase<AccountId>
{
    public const int Length = 12;

    public ClientId? ClientId { get; private set; }
    public LiveOrTest Mode { get; private set; }
    public int Ordinal { get; private set; }

    protected override void SetProperties(string input)
    {
        ClientId = ClientId.Create(input[0..8]);
        Mode = input[8].ToLiveOrTest();
        Ordinal = int.Parse(input[9..]);
    }

    public static bool IsInput(string input)
    {
        if (input is null)
            return false;

        if (input.Length != Length)
            return false;

        if (!input[0..8].IsClientIdInput())
            return false;

        if (!input[8].IsLiveOrTestCode())
            return false;

        if (!int.TryParse(input[9..], out int ordinal))
            return false;

        return ordinal >= 1 && ordinal <= 255;
    }

    public static AccountId Create(string input) =>
        DoCreate(input, IsInput);

    public static bool TryCreate(string input, out AccountId result) =>
        DoTryCreate(input, IsInput, out result);
}