using System.Diagnostics;

namespace CuteUtils.Misc;

/// <summary>
/// Provides methods for safely retrying actions and functions with optional delay and retry count.
/// </summary>
public static class Try
{
    /// <summary>
    /// Retries an asynchronous function returning a value, up to a specified number of times, with an optional delay between attempts.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="delay">The delay between retry attempts. Default is 1 second.</param>
    /// <returns>The result of the function if successful.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if all retry attempts fail.</exception>
    public static async Task<T> RetryAsync<T>(Func<Task<T>> action, int maxRetries = 3, TimeSpan? delay = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        delay ??= TimeSpan.FromSeconds(1);
        Exception? lastException = null;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                lastException = ex;
                await Task.Delay(delay.Value);
            }
        }
        throw new InvalidOperationException($"Failed after {maxRetries} attempts.", lastException);
    }

    /// <summary>
    /// Retries an asynchronous action up to a specified number of times, with an optional delay between attempts.
    /// </summary>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="delay">The delay between retry attempts. Default is 1 second.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if all retry attempts fail.</exception>
    public static async Task RetryAsync(Func<Task> action, int maxRetries = 3, TimeSpan? delay = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        delay ??= TimeSpan.FromSeconds(1);
        Exception? lastException = null;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                await action();
                return;
            }
            catch (Exception ex)
            {
                lastException = ex;
                await Task.Delay(delay.Value);
            }
        }
        throw new InvalidOperationException($"Failed after {maxRetries} attempts.", lastException);
    }

    /// <summary>
    /// Retries a synchronous function returning a value, up to a specified number of times, with an optional delay between attempts.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="delay">The delay between retry attempts. Default is 1 second.</param>
    /// <returns>The result of the function if successful.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if all retry attempts fail.</exception>
    public static T Retry<T>(Func<T> action, int maxRetries = 3, TimeSpan? delay = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        delay ??= TimeSpan.FromSeconds(1);
        Exception? lastException = null;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                lastException = ex;
                System.Threading.Thread.Sleep(delay.Value);
            }
        }
        throw new InvalidOperationException($"Failed after {maxRetries} attempts.", lastException);
    }

    /// <summary>
    /// Retries a synchronous action up to a specified number of times, with an optional delay between attempts.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="maxRetries">The maximum number of retry attempts. Default is 3.</param>
    /// <param name="delay">The delay between retry attempts. Default is 1 second.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if all retry attempts fail.</exception>
    public static void Retry(Action action, int maxRetries = 3, TimeSpan? delay = null)
    {
        ArgumentNullException.ThrowIfNull(action);

        delay ??= TimeSpan.FromSeconds(1);
        Exception? lastException = null;
        for (int i = 0; i < maxRetries; i++)
        {
            try
            {
                action();
                return;
            }
            catch (Exception ex)
            {
                lastException = ex;
                System.Threading.Thread.Sleep(delay.Value);
            }
        }
        throw new InvalidOperationException($"Failed after {maxRetries} attempts.", lastException);
    }

    /// <summary>
    /// Executes a synchronous action and catches any exception, writing it to the debug output.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public static void Catch(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    /// <summary>
    /// Executes a synchronous action and catches any exception, writing it to the debug output and returning it via an out parameter.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="exception">When this method returns, contains the exception that was thrown, or null if no exception was thrown.</param>
    public static void Catch(Action action, out Exception? exception)
    {
        exception = null;
        try
        {
            action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            exception = ex;
        }
    }

    /// <summary>
    /// Executes a synchronous function and catches any exception, writing it to the debug output. Returns the default value if an exception occurs.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <returns>The result of the function, or the default value of <typeparamref name="T"/> if an exception occurs.</returns>
    public static T? Catch<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default;
        }
    }

    /// <summary>
    /// Executes a synchronous function and catches any exception, writing it to the debug output and returning the exception via an out parameter.
    /// Returns the default value if an exception occurs.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="action">The function to execute.</param>
    /// <param name="exception">When this method returns, contains the exception that was thrown, or null if no exception was thrown.</param>
    /// <returns>The result of the function, or the default value of <typeparamref name="T"/> if an exception occurs.</returns>
    public static T? Catch<T>(Func<T> action, out Exception? exception)
    {
        exception = null;
        try
        {
            return action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            exception = ex;
            return default;
        }
    }

    /// <summary>
    /// Executes an asynchronous action and catches any exception, writing it to the debug output.
    /// </summary>
    /// <param name="action">The asynchronous action to execute.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task CatchAsync(Func<Task> action)
    {
        try
        {
            await action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    /// <summary>
    /// Executes an asynchronous function and catches any exception, writing it to the debug output.
    /// Returns the default value if an exception occurs.
    /// </summary>
    /// <typeparam name="T">The return type of the function.</typeparam>
    /// <param name="action">The asynchronous function to execute.</param>
    /// <returns>The result of the function, or the default value of <typeparamref name="T"/> if an exception occurs.</returns>
    public static async Task<T?> CatchAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default;
        }
    }

    /// <summary>
    /// Waits for an asynchronous task to complete and catches any exception, writing it to the debug output.
    /// </summary>
    /// <param name="task">The task to await.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public static async Task CatchAsync(Task task)
    {
        try
        {
            await task;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
        }
    }

    /// <summary>
    /// Waits for an asynchronous task that returns a result and catches any exception, writing it to the debug output.
    /// Returns the default value if an exception occurs.
    /// </summary>
    /// <typeparam name="T">The return type of the task.</typeparam>
    /// <param name="task">The task to await.</param>
    /// <returns>The result of the task, or the default value of <typeparamref name="T"/> if an exception occurs.</returns>
    public static async Task<T?> CatchAsync<T>(Task<T> task)
    {
        try
        {
            return await task;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return default;
        }
    }
}