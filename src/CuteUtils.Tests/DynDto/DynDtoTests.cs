using CuteUtils.Reflection;

using System.Dynamic;

namespace CuteUtils.Tests.DynDto;

[TestClass]
public class DynDtoTests
{
    [TestMethod]
    public void ToDto_ShouldConvertObjectToDynamicDto()
    {
        // Arrange
        TestData data = new TestData
        {
            Name = "John",
            Age = 30
        };

        // Act
        dynamic dto = data.ToDto();

        // Assert
        Assert.AreEqual("John", dto.Name);
        Assert.AreEqual(30, dto.Age);
    }

    [TestMethod]
    public void ToDto_ShouldConvertObjectToSpecifiedDtoType()
    {
        // Arrange
        TestData data = new TestData
        {
            Name = "John",
            Age = 30
        };
        TestDataDto dto = new TestDataDto();

        // Act
        dto = data.ToDto(dto);

        // Assert
        Assert.AreEqual("John", dto.Name);
        Assert.AreEqual(30, dto.Age);
    }

    [TestMethod]
    public void ToDto_ShouldConvertObjectToNewInstanceDto()
    {
        // Arrange
        TestData data = new TestData
        {
            Name = "John",
            Age = 30
        };

        // Act
        TestDataDto dto = data.ToDto<TestDataDto>();

        // Assert
        Assert.AreEqual("John", dto.Name);
        Assert.AreEqual(30, dto.Age);
    }

    [TestMethod]
    public void FromDto_ShouldConvertDynamicDtoToObject()
    {
        // Arrange
        ExpandoObject dto = new ExpandoObject();
        _ = dto.TryAdd("Name", "John");
        _ = dto.TryAdd("Age", 30);
        TestData data = new TestData();

        // Act
        data = dto.FromDto(data);

        // Assert
        Assert.AreEqual("John", data.Name);
        Assert.AreEqual(30, data.Age);
    }

    [TestMethod]
    public void FromDto_ShouldConvertDynamicDtoToNewInstanceObject()
    {
        // Arrange
        ExpandoObject dto = new ExpandoObject();
        _ = dto.TryAdd("Name", "John");
        _ = dto.TryAdd("Age", 30);

        // Act
        TestData data = dto.FromDto<TestData>();

        // Assert
        Assert.AreEqual("John", data.Name);
        Assert.AreEqual(30, data.Age);
    }

    [TestMethod]
    public void FromDto_ShouldConvertDtoToObject()
    {
        // Arrange
        TestDataDto dto = new TestDataDto
        {
            Name = "John",
            Age = 30
        };
        TestData data = new TestData();

        // Act
        data = dto.FromDto(data);

        // Assert
        Assert.AreEqual("John", data.Name);
        Assert.AreEqual(30, data.Age);
    }

    [TestMethod]
    public void FromDto_ShouldConvertDtoToNewInstanceObject()
    {
        // Arrange
        TestDataDto dto = new TestDataDto
        {
            Name = "John",
            Age = 30
        };

        // Act
        TestData data = dto.FromDto<TestData>();

        // Assert
        Assert.AreEqual("John", data.Name);
        Assert.AreEqual(30, data.Age);
    }

    public class TestData
    {
        [DynDtoName("Name")]
        public string Name { get; set; } = string.Empty;

        [DynDtoName("Age")]
        public int Age { get; set; }
    }

    public class TestDataDto
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}