namespace CuteUtils.Misc;

/// <summary>
/// Provides methods for safely retrying actions and functions with optional delay and retry count.
/// </summary>
public static class SafeRetry
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
}