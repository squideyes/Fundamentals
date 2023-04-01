// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

namespace SquidEyes.Fundamentals;

public static class TaskExtenders
{
    // Task.Run(() => { ... }).Forget();
    public static void Forget(this Task task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task));

        if (!task.IsCompleted || task.IsFaulted)
            _ = ForgetAwaited(task);

        async static Task ForgetAwaited(Task task)
        {
            try
            {
                await task.ConfigureAwait(false);
            }
            catch
            {
            }
        }
    }
}