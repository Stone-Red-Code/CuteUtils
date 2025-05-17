using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class WaitTests
{
    [TestMethod]
    public async Task UntilAsync_ConditionMet_CompletesSuccessfully()
    {
        bool flag = false;
        _ = Task.Run(() =>
        {
            Thread.Sleep(200);
            flag = true;
        });
        await Wait.UntilAsync(() => flag, TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(2));
        Assert.IsTrue(flag);
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public async Task UntilAsync_ConditionNotMet_ThrowsTimeoutException()
    {
        await Wait.UntilAsync(() => false, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(200));
    }

    [TestMethod]
    public void Until_ConditionMet_CompletesSuccessfully()
    {
        bool flag = false;
        _ = Task.Run(() =>
        {
            Thread.Sleep(200);
            flag = true;
        });
        Wait.Until(() => flag, TimeSpan.FromMilliseconds(50), TimeSpan.FromSeconds(2));
        Assert.IsTrue(flag);
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public void Until_ConditionNotMet_ThrowsTimeoutException()
    {
        Wait.Until(() => false, TimeSpan.FromMilliseconds(50), TimeSpan.FromMilliseconds(200));
    }

    [TestMethod]
    public async Task WaitForEventAsync_EventRaised_CompletesSuccessfully()
    {
        void subscribe(Action handler)
        {
            _ = Task.Run(() =>
            {
                Thread.Sleep(200);
                handler();
            });
        }
        await Wait.WaitForEventAsync(subscribe, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public async Task WaitForEventAsync_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action handler) { }
        await Wait.WaitForEventAsync(subscribe, TimeSpan.FromMilliseconds(200));
    }

    [TestMethod]
    public async Task WaitForEventAsyncT_EventRaised_CompletesWithValue()
    {
        void subscribe(Action<int> handler)
        {
            _ = Task.Run(() =>
            {
                Thread.Sleep(200);
                handler(42);
            });
        }
        int result = await Wait.WaitForEventAsync((Action<Action<int>>)subscribe, TimeSpan.FromSeconds(2));
        Assert.AreEqual(42, result);
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public async Task WaitForEventAsyncT_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action<int> handler) { }
        _ = await Wait.WaitForEventAsync((Action<Action<int>>)subscribe, TimeSpan.FromMilliseconds(200));
    }

    [TestMethod]
    public void WaitForEvent_EventRaised_CompletesSuccessfully()
    {
        void subscribe(Action handler)
        {
            _ = Task.Run(() =>
            {
                Thread.Sleep(200);
                handler();
            });
        }
        Wait.WaitForEvent(subscribe, TimeSpan.FromSeconds(2));
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public void WaitForEvent_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action handler) { }
        Wait.WaitForEvent(subscribe, TimeSpan.FromMilliseconds(200));
    }

    [TestMethod]
    public void WaitForEventT_EventRaised_CompletesWithValue()
    {
        void subscribe(Action<string> handler)
        {
            _ = Task.Run(() =>
            {
                Thread.Sleep(200);
                handler("hello");
            });
        }
        string result = Wait.WaitForEvent((Action<Action<string>>)subscribe, TimeSpan.FromSeconds(2));
        Assert.AreEqual("hello", result);
    }

    [TestMethod]
    [ExpectedException(typeof(TimeoutException))]
    public void WaitForEventT_EventNotRaised_ThrowsTimeoutException()
    {
        void subscribe(Action<string> handler) { }
        _ = Wait.WaitForEvent((Action<Action<string>>)subscribe, TimeSpan.FromMilliseconds(200));
    }
}