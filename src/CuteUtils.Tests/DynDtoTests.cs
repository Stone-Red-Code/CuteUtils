using CuteUtils.Reflection;

using System.Dynamic;

namespace CuteUtils.Tests;

[TestClass]
public class DynDtoTests
{
    [TestMethod]
    public void Convert_ToDto_ReturnsExpandoObject()
    {
        TestModel testModel = new()
        {
            Id = 13,
            Name = "test",
            Value = (15, "test"),
        };

        ExpandoObject dto = testModel.ToDto();

        Assert.IsNotNull(dto);

        Assert.IsFalse(dto.Any(p => p.Key == "Id"));
        Assert.IsTrue(dto.Any(p => p.Key == "Name"));
        Assert.IsTrue(dto.Any(p => p.Key == "Value"));
    }

    [TestMethod]
    public void Convert_ToDto_ReturnsGeneric()
    {
        TestModel testModel = new()
        {
            Id = 13,
            Name = "test",
            Value = (15, "test"),
        };

        TestDto dto = testModel.ToDto<TestDto>();

        Assert.IsNotNull(dto);

        Assert.IsTrue(dto.Id == 0);
        Assert.IsTrue(!string.IsNullOrWhiteSpace(dto.Name));
        Assert.IsTrue(dto.Value is not (0, ""));
    }

    private class TestModel
    {
        public required int Id { get; set; }

        [DynDtoName(nameof(Name))]
        public required string Name { get; set; }

        [DynDtoName(nameof(Value))]
        public required object Value { get; set; }
    }

    private class TestDto
    {
        public int Id { get; init; } = 0;
        public string Name { get; set; } = string.Empty;
        public object Value { get; init; } = (0, string.Empty);
    }
}