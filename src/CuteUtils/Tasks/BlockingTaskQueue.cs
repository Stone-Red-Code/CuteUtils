namespace CuteUtils.Tasks;

/// <summary>
/// Represents a blocking task queue that allows enqueueing tasks and functions.
/// </summary>
public class BlockingTaskQueue
{
    private readonly SemaphoreSlim semaphore;

    /// <summary>
    /// Initializes a new instance of the <see cref="BlockingTaskQueue"/> class.
    /// </summary>
    public BlockingTaskQueue()
    {
        semaphore = new SemaphoreSlim(1);
    }

    /// <summary>
    /// Enqueues a task that returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="function">The function to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<T> Enqueue<T>(Func<T> function)
    {
        await semaphore.WaitAsync();
        try
        {
            return await Task.Run(function);
        }
        finally
        {
            _ = semaphore.Release();
        }
    }

    /// <summary>
    /// Enqueues a task that does not return a value.
    /// </summary>
    /// <param name="function">The action to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Enqueue(Action function)
    {
        await semaphore.WaitAsync();
        try
        {
            await Task.Run(function);
        }
        finally
        {
            _ = semaphore.Release();
        }
    }

    /// <summary>
    /// Enqueues a task.
    /// </summary>
    /// <param name="task">The task to enqueue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Enqueue(Task task)
    {
        await semaphore.WaitAsync();
        try
        {
            await task;
        }
        finally
        {
            _ = semaphore.Release();
        }
    }

    /// <summary>
    /// Enqueues a task that returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the return value.</typeparam>
    /// <param name="task">The task to enqueue.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task<T> Enqueue<T>(Task<T> task)
    {
        await semaphore.WaitAsync();
        try
        {
            return await task;
        }
        finally
        {
            _ = semaphore.Release();
        }
    }
}