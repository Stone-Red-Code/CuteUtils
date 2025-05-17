using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class SafeRetryTests
{
    [TestMethod]
    public async Task RetryAsyncT_SucceedsFirstTry()
    {
        int callCount = 0;
        int result = await SafeRetry.RetryAsync(async () =>
        {
            callCount++;
            await Task.Delay(10);
            return 42;
        });
        Assert.AreEqual(1, callCount);
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public async Task RetryAsyncT_RetriesAndSucceeds()
    {
        int callCount = 0;
        int result = await SafeRetry.RetryAsync(async () =>
        {
            callCount++;
            if (callCount < 3)
            {
                throw new InvalidOperationException();
            }

            await Task.Delay(10);
            return 99;
        }, maxRetries: 5, delay: TimeSpan.FromMilliseconds(1));
        Assert.AreEqual(3, callCount);
        Assert.AreEqual(99, result);
    }

    [TestMethod]
    public async Task RetryAsyncT_ThrowsAfterMaxRetries()
    {
        int callCount = 0;
        _ = await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
        {
            _ = await SafeRetry.RetryAsync<int>(() =>
            {
                callCount++;
                throw new InvalidOperationException("fail");
            }, maxRetries: 2, delay: TimeSpan.FromMilliseconds(1));
        });
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public async Task RetryAsync_SucceedsFirstTry()
    {
        int callCount = 0;
        await SafeRetry.RetryAsync(async () =>
        {
            callCount++;
            await Task.Delay(10);
        });
        Assert.AreEqual(1, callCount);
    }

    [TestMethod]
    public async Task RetryAsync_RetriesAndSucceeds()
    {
        int callCount = 0;
        await SafeRetry.RetryAsync(async () =>
        {
            callCount++;
            if (callCount < 2)
            {
                throw new Exception();
            }

            await Task.Delay(10);
        }, maxRetries: 3, delay: TimeSpan.FromMilliseconds(1));
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public async Task RetryAsync_ThrowsAfterMaxRetries()
    {
        int callCount = 0;
        _ = await Assert.ThrowsExceptionAsync<InvalidOperationException>(async () =>
        {
            await SafeRetry.RetryAsync(() =>
            {
                callCount++;
                throw new Exception();
            }, maxRetries: 2, delay: TimeSpan.FromMilliseconds(1));
        });
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public void RetryT_SucceedsFirstTry()
    {
        int callCount = 0;
        int result = SafeRetry.Retry(() =>
        {
            callCount++;
            return 7;
        });
        Assert.AreEqual(1, callCount);
        Assert.AreEqual(7, result);
    }

    [TestMethod]
    public void RetryT_RetriesAndSucceeds()
    {
        int callCount = 0;
        int result = SafeRetry.Retry(() =>
        {
            callCount++;
            if (callCount < 4)
            {
                throw new Exception();
            }

            return 123;
        }, maxRetries: 5, delay: TimeSpan.FromMilliseconds(1));
        Assert.AreEqual(4, callCount);
        Assert.AreEqual(123, result);
    }

    [TestMethod]
    public void RetryT_ThrowsAfterMaxRetries()
    {
        int callCount = 0;
        _ = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            _ = SafeRetry.Retry<int>(() =>
            {
                callCount++;
                throw new Exception();
            }, maxRetries: 3, delay: TimeSpan.FromMilliseconds(1));
        });
        Assert.AreEqual(3, callCount);
    }

    [TestMethod]
    public void Retry_SucceedsFirstTry()
    {
        int callCount = 0;
        SafeRetry.Retry(() =>
        {
            callCount++;
        });
        Assert.AreEqual(1, callCount);
    }

    [TestMethod]
    public void Retry_RetriesAndSucceeds()
    {
        int callCount = 0;
        SafeRetry.Retry(() =>
        {
            callCount++;
            if (callCount < 2)
            {
                throw new Exception();
            }
        }, maxRetries: 3, delay: TimeSpan.FromMilliseconds(1));
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public void Retry_ThrowsAfterMaxRetries()
    {
        int callCount = 0;
        _ = Assert.ThrowsException<InvalidOperationException>(() =>
        {
            SafeRetry.Retry(() =>
            {
                callCount++;
                throw new Exception();
            }, maxRetries: 2, delay: TimeSpan.FromMilliseconds(1));
        });
        Assert.AreEqual(2, callCount);
    }

    [TestMethod]
    public async Task RetryAsyncT_ThrowsArgumentNullException()
    {
        _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            _ = await SafeRetry.RetryAsync<int>(null!);
        });
    }

    [TestMethod]
    public async Task RetryAsync_ThrowsArgumentNullException()
    {
        _ = await Assert.ThrowsExceptionAsync<ArgumentNullException>(async () =>
        {
            await SafeRetry.RetryAsync(null!);
        });
    }

    [TestMethod]
    public void RetryT_ThrowsArgumentNullException()
    {
        _ = Assert.ThrowsException<ArgumentNullException>(() =>
        {
            _ = SafeRetry.Retry<int>(null!);
        });
    }

    [TestMethod]
    public void Retry_ThrowsArgumentNullException()
    {
        _ = Assert.ThrowsException<ArgumentNullException>(() =>
        {
            SafeRetry.Retry(null!);
        });
    }
}