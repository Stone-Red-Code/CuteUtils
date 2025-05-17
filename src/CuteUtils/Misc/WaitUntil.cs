namespace CuteUtils.Misc;

/// <summary>
/// Provides utility methods for waiting on conditions or events with optional timeouts and intervals.
/// </summary>
public static class Wait
{
    /// <summary>
    /// Asynchronously waits until the specified condition is met or the timeout is reached.
    /// </summary>
    /// <param name="condition">A function that returns true when the wait should end.</param>
    /// <param name="interval">The interval to check the condition. Defaults to 100ms.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <exception cref="TimeoutException">Thrown if the condition is not met within the timeout.</exception>
    public static async Task UntilAsync(Func<bool> condition, TimeSpan? interval = null, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;
        interval ??= TimeSpan.FromMilliseconds(100);
        DateTime endTime = DateTime.UtcNow.Add(timeout.Value);
        while (!condition() && DateTime.UtcNow < endTime)
        {
            await Task.Delay(interval.Value);
        }
        if (!condition())
        {
            throw new TimeoutException("The condition was not met within the specified timeout.");
        }
    }

    /// <summary>
    /// Synchronously waits until the specified condition is met or the timeout is reached.
    /// </summary>
    /// <param name="condition">A function that returns true when the wait should end.</param>
    /// <param name="interval">The interval to check the condition. Defaults to 100ms.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <exception cref="TimeoutException">Thrown if the condition is not met within the timeout.</exception>
    public static void Until(Func<bool> condition, TimeSpan? interval = null, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;
        interval ??= TimeSpan.FromMilliseconds(100);
        DateTime endTime = DateTime.UtcNow.Add(timeout.Value);
        while (!condition() && DateTime.UtcNow < endTime)
        {
            System.Threading.Thread.Sleep(interval.Value);
        }
        if (!condition())
        {
            throw new TimeoutException("The condition was not met within the specified timeout.");
        }
    }

    /// <summary>
    /// Asynchronously waits for an event to be raised or the timeout to be reached.
    /// </summary>
    /// <param name="subscribe">An action that subscribes a handler to the event.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <exception cref="TimeoutException">Thrown if the event is not raised within the timeout.</exception>
    public static async Task WaitForEventAsync(Action<Action> subscribe, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;
        TaskCompletionSource tcs = new TaskCompletionSource();
        void Handler()
        {
            _ = tcs.TrySetResult();
        }

        subscribe(Handler);

        using CancellationTokenSource cts = new CancellationTokenSource(timeout.Value);
        using (cts.Token.Register(() => tcs.TrySetCanceled(cts.Token)))
        {
            try
            {
                await tcs.Task.ConfigureAwait(false);
            }
            catch (TaskCanceledException)
            {
                throw new TimeoutException("The event was not raised within the specified timeout.");
            }
        }
    }

    /// <summary>
    /// Asynchronously waits for an event to be raised or the timeout to be reached.
    /// </summary>
    /// <param name="subscribe">An action that subscribes a handler to the event.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <typeparam name="T">The type of the event argument.</typeparam>
    /// <returns>A task that completes with the event argument when the event is raised.</returns>
    /// <exception cref="TimeoutException">Thrown if the event is not raised within the timeout.</exception>
    public static async Task<T> WaitForEventAsync<T>(Action<Action<T>> subscribe, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;

        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();

        void Handler(T arg)
        {
            _ = tcs.TrySetResult(arg);
        }

        subscribe(Handler);

        using CancellationTokenSource cts = new CancellationTokenSource(timeout.Value);
        using (cts.Token.Register(() => tcs.TrySetCanceled(cts.Token)))
        {
            try
            {
                return await tcs.Task;
            }
            catch (TaskCanceledException)
            {
                throw new TimeoutException("The event was not raised within the specified timeout.");
            }
        }
    }

    /// <summary>
    /// Synchronously waits for an event to be raised or the timeout to be reached.
    /// </summary>
    /// <param name="subscribe">An action that subscribes a handler to the event.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <exception cref="TimeoutException">Thrown if the event is not raised within the timeout.</exception>
    public static void WaitForEvent(Action<Action> subscribe, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;
        TaskCompletionSource tcs = new TaskCompletionSource();
        void Handler()
        {
            _ = tcs.TrySetResult();
        }
        subscribe(Handler);
        using CancellationTokenSource cts = new CancellationTokenSource(timeout.Value);
        using (cts.Token.Register(() => tcs.TrySetCanceled(cts.Token)))
        {
            try
            {
                tcs.Task.Wait();
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                throw new TimeoutException("The event was not raised within the specified timeout.", ex);
            }
        }
    }

    /// <summary>
    /// Synchronously waits for an event to be raised or the timeout to be reached.
    /// </summary>
    /// <param name="subscribe">An action that subscribes a handler to the event.</param>
    /// <param name="timeout">The maximum time to wait. Defaults to infinite.</param>
    /// <typeparam name="T">The type of the event argument.</typeparam>
    /// <returns>The event argument when the event is raised.</returns>
    /// <exception cref="TimeoutException">Thrown if the event is not raised within the timeout.</exception>
    public static T WaitForEvent<T>(Action<Action<T>> subscribe, TimeSpan? timeout = null)
    {
        timeout ??= TimeSpan.MaxValue;
        TaskCompletionSource<T> tcs = new TaskCompletionSource<T>();
        void Handler(T arg)
        {
            _ = tcs.TrySetResult(arg);
        }
        subscribe(Handler);
        using CancellationTokenSource cts = new CancellationTokenSource(timeout.Value);
        using (cts.Token.Register(() => tcs.TrySetCanceled(cts.Token)))
        {
            try
            {
                return tcs.Task.GetAwaiter().GetResult();
            }
            catch (AggregateException ex) when (ex.InnerException is TaskCanceledException)
            {
                throw new TimeoutException("The event was not raised within the specified timeout.", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new TimeoutException("The event was not raised within the specified timeout.", ex);
            }
        }
    }
}