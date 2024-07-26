using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class BoolExtensionsTests
{
    [TestMethod]
    public void OneWayTrue_ShouldSetValueToTrue_WhenInputIsTrue()
    {
        // Arrange
        bool value = false;
        bool input = true;

        // Act
        value.OneWayTrue(input);

        // Assert
        Assert.IsTrue(value);
    }

    [TestMethod]
    public void OneWayTrue_ShouldNotChangeValue_WhenInputIsFalse()
    {
        // Arrange
        bool value = true;
        bool input = false;

        // Act
        value.OneWayTrue(input);

        // Assert
        Assert.IsTrue(value);
    }

    [TestMethod]
    public void OneWayFalse_ShouldSetValueToFalse_WhenInputIsFalse()
    {
        // Arrange
        bool value = true;
        bool input = false;

        // Act
        value.OneWayFalse(input);

        // Assert
        Assert.IsFalse(value);
    }

    [TestMethod]
    public void OneWayFalse_ShouldNotChangeValue_WhenInputIsTrue()
    {
        // Arrange
        bool value = false;
        bool input = true;

        // Act
        value.OneWayFalse(input);

        // Assert
        Assert.IsFalse(value);
    }

    [TestMethod]
    public void ToInt_ShouldReturn1_WhenInputIsTrue()
    {
        // Arrange
        bool input = true;

        // Act
        int result = input.ToInt();

        // Assert
        Assert.AreEqual(1, result);
    }

    [TestMethod]
    public void ToInt_ShouldReturn0_WhenInputIsFalse()
    {
        // Arrange
        bool input = false;

        // Act
        int result = input.ToInt();

        // Assert
        Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void FromInt_ShouldSetValueToTrue_WhenInputIs1()
    {
        // Arrange
        bool value = false;
        int input = 1;

        // Act
        value.FromInt(input);

        // Assert
        Assert.IsTrue(value);
    }

    [TestMethod]
    public void FromInt_ShouldSetValueToFalse_WhenInputIs0()
    {
        // Arrange
        bool value = true;
        int input = 0;

        // Act
        value.FromInt(input);

        // Assert
        Assert.IsFalse(value);
    }
}