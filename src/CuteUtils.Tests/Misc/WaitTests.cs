using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class WaitTests
{
    [TestMethod]
    public async Task UntilAsync_ConditionMet_CompletesSuccessfully()
    {
        bool flag = false;
        _ = Task.Run(async () =>
        {
            await Task.Delay(200);
            flag = true;
        });
        await Wait.UntilAsync(() => flag, TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(2));
        Assert.IsTrue(flag);
    }

    [TestMethod]
    public async Task UntilAsync_ConditionNotMet_ThrowsTimeoutException()
    {
        _ = await Assert.ThrowsExactlyAsync<TimeoutException>(async () =>
        {
            await Wait.UntilAsync(() => false, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(200));
        });
    }

    [TestMethod]
    public void Until_ConditionMet_CompletesSuccessfully()
    {
        bool flag = false;
        _ = Task.Run(async () =>
        {
            await Task.Delay(200);
            flag = true;
        });
        Wait.Until(() => flag, TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(2));
        Assert.IsTrue(flag);
    }

    [TestMethod]
    public void Until_ConditionNotMet_ThrowsTimeoutException()
    {
        _ = Assert.ThrowsExactly<TimeoutException>(() =>
        {
            Wait.Until(() => false, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(200));
        });
    }

    [TestMethod]
    public async Task WaitForEventAsync_EventRaised_CompletesSuccessfully()
    {
        void subscribe(Action handler)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(200);
                handler();
            });
        }
        await Wait.WaitForEventAsync(subscribe, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    public async Task WaitForEventAsync_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action handler)
        {
            // Intentionally left empty for negative test case (event never raised)
        }
        _ = await Assert.ThrowsExactlyAsync<TimeoutException>(async () =>
        {
            await Wait.WaitForEventAsync(subscribe, TimeSpan.FromMilliseconds(200));
        });
    }

    [TestMethod]
    public async Task WaitForEventAsyncT_EventRaised_CompletesWithValue()
    {
        void subscribe(Action<int> handler)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(200);
                handler(42);
            });
        }
        int result = await Wait.WaitForEventAsync((Action<Action<int>>)subscribe, TimeSpan.FromSeconds(2));
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    public async Task WaitForEventAsyncT_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action<int> handler)
        {
            // Intentionally left empty for negative test case (event never raised)
        }
        _ = await Assert.ThrowsExactlyAsync<TimeoutException>(async () =>
        {
            _ = await Wait.WaitForEventAsync((Action<Action<int>>)subscribe, TimeSpan.FromMilliseconds(200));
        });
    }

    [TestMethod]
    public void WaitForEvent_EventRaised_CompletesSuccessfully()
    {
        void subscribe(Action handler)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(200);
                handler();
            });
        }
        Wait.WaitForEvent(subscribe, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    public void WaitForEvent_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action handler)
        {
            // Intentionally left empty for negative test case (event never raised)
        }
        _ = Assert.ThrowsExactly<TimeoutException>(() =>
        {
            Wait.WaitForEvent(subscribe, TimeSpan.FromMilliseconds(200));
        });
    }

    [TestMethod]
    public void WaitForEventT_EventRaised_CompletesWithValue()
    {
        void subscribe(Action<string> handler)
        {
            _ = Task.Run(async () =>
            {
                await Task.Delay(200);
                handler("hello");
            });
        }
        string result = Wait.WaitForEvent((Action<Action<string>>)subscribe, TimeSpan.FromSeconds(2));
        Assert.AreEqual("hello", result);
    }

    [TestMethod]
    public void WaitForEventT_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action<string> handler)
        {
            // Intentionally left empty for negative test case (event never raised)
        }
        _ = Assert.ThrowsExactly<TimeoutException>(() =>
        {
            _ = Wait.WaitForEvent((Action<Action<string>>)subscribe, TimeSpan.FromMilliseconds(200));
        });
    }
}