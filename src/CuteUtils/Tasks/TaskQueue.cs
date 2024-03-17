using System.Collections.Concurrent;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace CuteUtils.Tasks;

/// <summary>
/// Represents a queue of tasks that can be enqueued and processed asynchronously.
/// </summary>
public class TaskQueue : IDisposable
{
    private readonly BlockingCollection<(Task Task, Action Callback)> tasks = [];
    private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private bool processing = false;
    private bool disposed;

    /// <summary>
    /// Enqueues a task that returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by the task.</typeparam>
    /// <param name="function">The function representing the task.</param>
    /// <returns>An observable that emits the task when it completes.</returns>
    public IObservable<Task<T>> Enqueue<T>(Func<T> function)
    {
        Subject<Task<T>> subject = new Subject<Task<T>>();
        Task<T> task = new Task<T>(function);

        tasks.Add((task, () => subject.OnNext(task)));

        ProcessTasks();

        return subject.AsObservable();
    }

    /// <summary>
    /// Enqueues a task that does not return a value.
    /// </summary>
    /// <param name="function">The action representing the task.</param>
    /// <returns>An observable that emits the task when it completes.</returns>
    public IObservable<Task> Enqueue(Action function)
    {
        Subject<Task> subject = new Subject<Task>();
        Task task = new Task(function);

        tasks.Add((task, () => subject.OnNext(task)));

        ProcessTasks();

        return subject.AsObservable();
    }

    /// <summary>
    /// Enqueues a pre-created task.
    /// </summary>
    /// <param name="task">The task to enqueue.</param>
    /// <returns>An observable that emits the task when it completes.</returns>
    public IObservable<Task> Enqueue(Task task)
    {
        Subject<Task> subject = new Subject<Task>();

        tasks.Add((task, () => subject.OnNext(task)));

        ProcessTasks();

        return subject.AsObservable();
    }

    /// <summary>
    /// Enqueues a pre-created task that returns a value.
    /// </summary>
    /// <typeparam name="T">The type of the value returned by the task.</typeparam>
    /// <param name="task">The task to enqueue.</param>
    /// <returns>An observable that emits the task when it completes.</returns>
    public IObservable<Task<T>> Enqueue<T>(Task<T> task)
    {
        Subject<Task<T>> subject = new Subject<Task<T>>();

        tasks.Add((task, () => subject.OnNext(task)));

        ProcessTasks();

        return subject.AsObservable();
    }

    /// <summary>
    /// Disposes the task queue and cancels any pending tasks.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                cancellationTokenSource.Dispose();
                tasks.Dispose();
            }

            disposed = true;
        }
    }

    private void ProcessTasks()
    {
        if (processing)
        {
            return;
        }

        processing = true;

        _ = new TaskFactory().StartNew(async () =>
        {
            while (!disposed)
            {
                (Task Task, Action Callback) container = tasks.Take(cancellationTokenSource.Token);

                if (container.Task.Status == TaskStatus.Created)
                {
                    container.Task.Start();
                }

                await container.Task.WaitAsync(cancellationTokenSource.Token);
                container.Callback?.Invoke();
            }
        }, cancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
    }
}