// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using static System.Threading.Tasks.TaskCreationOptions;

namespace SquidEyes.Fundamentals;

public abstract class DisposeBase : IDisposable
{
    private readonly CancellationTokenSource cts;
    private readonly TaskCompletionSource<object?> tcs;

    private long disposing;

    protected DisposeBase()
    {
        cts = new();

        DisposedToken = cts.Token;

        tcs = new(RunContinuationsAsynchronously);

        DisposedTask = tcs.Task;
    }

    public CancellationToken DisposedToken { get; }

    public Task DisposedTask { get; }

    public bool IsDisposeStarted => Interlocked.Read(ref disposing) == 1;

    public Exception? DisposalReason { get; private set; }

    public void KickoffDispose(Exception? disposalReason = null) =>
        Task.Run(() => Dispose(disposalReason)).Ignore();

    public void Dispose() =>
        Dispose(new ObjectDisposedException("Object has been disposed."));

    public void Dispose(Exception? disposalReason)
    {
        if (Interlocked.Exchange(ref disposing, 1) == 1)
            return;

        DisposalReason = disposalReason;

        cts.Cancel();

        cts.Dispose();

        CustomDispose();

        tcs.SetResult(null);
    }

    protected virtual void CustomDispose()
    {
    }
}