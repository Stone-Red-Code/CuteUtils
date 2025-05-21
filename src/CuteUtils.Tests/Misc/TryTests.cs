using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class TryTests
{
    [TestMethod]
    public async Task RetryAsyncT_SucceedsFirstTry()
    {
        int callCount = 0;
        int result = await Try.RetryAsync(async () =>
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
        int result = await Try.RetryAsync(async () =>
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
        _ = await Assert.ThrowsExactlyAsync<InvalidOperationException>(async () =>
        {
            _ = await Try.RetryAsync<int>(() =>
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
        await Try.RetryAsync(async () =>
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
        await Try.RetryAsync(async () =>
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
        _ = await Assert.ThrowsExactlyAsync<InvalidOperationException>(async () =>
        {
            await Try.RetryAsync(() =>
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
        int result = Try.Retry(() =>
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
        int result = Try.Retry(() =>
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
        _ = Assert.ThrowsExactly<InvalidOperationException>(() =>
        {
            _ = Try.Retry<int>(() =>
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
        Try.Retry(() =>
        {
            callCount++;
        });
        Assert.AreEqual(1, callCount);
    }

    [TestMethod]
    public void Retry_RetriesAndSucceeds()
    {
        int callCount = 0;
        Try.Retry(() =>
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
        _ = Assert.ThrowsExactly<InvalidOperationException>(() =>
        {
            Try.Retry(() =>
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
        _ = await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            _ = await Try.RetryAsync<int>(null!);
        });
    }

    [TestMethod]
    public async Task RetryAsync_ThrowsArgumentNullException()
    {
        _ = await Assert.ThrowsExactlyAsync<ArgumentNullException>(async () =>
        {
            await Try.RetryAsync(null!);
        });
    }

    [TestMethod]
    public void RetryT_ThrowsArgumentNullException()
    {
        _ = Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            _ = Try.Retry<int>(null!);
        });
    }

    [TestMethod]
    public void Retry_ThrowsArgumentNullException()
    {
        _ = Assert.ThrowsExactly<ArgumentNullException>(() =>
        {
            Try.Retry(null!);
        });
    }

    [TestClass]
    public class TryCatchTests
    {
        [TestMethod]
        public void Catch_Action_NoException()
        {
            bool executed = false;

            _ = Try.Catch(() => executed = true);

            Assert.IsTrue(executed);
        }

        [TestMethod]
        public void Catch_Action_ExceptionHandled()
        {
            Try.Catch(() => throw new InvalidOperationException());
        }

        [TestMethod]
        public void Catch_Action_OutParam_ExceptionHandled()
        {
            Try.Catch(() => throw new InvalidOperationException(), out Exception? exception);

            Assert.IsNotNull(exception);
            Assert.IsInstanceOfType(exception, typeof(InvalidOperationException));
        }

        [TestMethod]
        public void Catch_TFunc_NoException()
        {
            int result = Try.Catch(() => 42)!;

            Assert.AreEqual(42, result);
        }

        [TestMethod]
        public void Catch_TFunc_ExceptionHandled_ReturnsDefault()
        {
            int? result = Try.Catch<int>(() => throw new InvalidOperationException());

            Assert.AreEqual(default(int), result);
        }

        [TestMethod]
        public void Catch_TFunc_OutParam_ExceptionHandled()
        {
            int? result = Try.Catch<int?>(() => throw new InvalidOperationException(), out Exception? exception);

            Assert.IsNull(result);
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public async Task CatchAsync_Delegate_NoException()
        {
            bool executed = false;

            await Try.CatchAsync(async () =>
            {
                executed = true;
                await Task.Delay(10);
            });

            Assert.IsTrue(executed);
        }

        [TestMethod]
        public async Task CatchAsync_Delegate_ExceptionHandled()
        {
            await Try.CatchAsync(async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException();
            });
        }

        [TestMethod]
        public async Task CatchAsync_TFunc_NoException()
        {
            int result = await Try.CatchAsync(async () =>
            {
                await Task.Delay(10);
                return 99;
            });

            Assert.AreEqual(99, result);
        }

        [TestMethod]
        public async Task CatchAsync_TFunc_ExceptionHandled_ReturnsDefault()
        {
            int result = await Try.CatchAsync<int>(async () =>
            {
                await Task.Delay(10);
                throw new InvalidOperationException();
            });

            Assert.AreEqual(default, result);
        }

        [TestMethod]
        public async Task CatchAsync_Task_NoException()
        {
            await Try.CatchAsync(Task.Delay(10));
        }

        [TestMethod]
        public async Task CatchAsync_Task_ExceptionHandled()
        {
            await Try.CatchAsync(Task.Run(() => throw new InvalidOperationException()));
        }

        [TestMethod]
        public async Task CatchAsync_TaskT_NoException()
        {
            string? result = await Try.CatchAsync(Task.FromResult("Hello"));

            Assert.AreEqual("Hello", result);
        }

        [TestMethod]
        public async Task CatchAsync_TaskT_ExceptionHandled_ReturnsDefault()
        {
            string? result = await Try.CatchAsync<string>(Task.Run<string>(() => ""[0].ToString()));

            Assert.IsNull(result);
        }
    }
}