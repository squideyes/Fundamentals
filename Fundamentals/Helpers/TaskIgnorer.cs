using static System.Threading.Tasks.TaskContinuationOptions;

namespace SquidEyes.Fundamentals;

internal static class TaskIgnorer
{
    private static readonly Action<Task> observeExeption = t => { _ = t.Exception; };

    public static void Ignore(this Task task)
    {
        if (task.IsCompleted)
        {
            _ = task.Exception;
        }
        else
        {
            task.ContinueWith(observeExeption, CancellationToken.None,
                OnlyOnFaulted | ExecuteSynchronously, TaskScheduler.Default);
        }
    }

    public static void Ignore(this ValueTask task)
    {
        if (task.IsCompleted)
        {
            try { task.GetAwaiter().GetResult(); } catch { }
        }
        else
        {
            task.AsTask().ContinueWith(observeExeption, CancellationToken.None,
                OnlyOnFaulted | ExecuteSynchronously, TaskScheduler.Default);
        }
    }

    public static void Ignore<T>(this ValueTask<T> task)
    {
        if (task.IsCompleted)
        {
            try { task.GetAwaiter().GetResult(); } catch { }
        }
        else
        {
            task.AsTask().ContinueWith(observeExeption, CancellationToken.None,
                OnlyOnFaulted | ExecuteSynchronously, TaskScheduler.Default);
        }
    }
}