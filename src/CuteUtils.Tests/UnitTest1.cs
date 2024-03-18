using CuteUtils.Reflection;

using System.Dynamic;

namespace CuteUtils.Tests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
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
    public void TestMethod2()
    {
        TestModel testModel = new()
        {
            Id = 13,
            Name = "test",
            Value = (15, "test"),
        };

        TestDto dto = testModel.ToDto<TestDto>();

        Assert.IsNotNull(dto);

        Assert.IsTrue(dto.Id != 0);
        Assert.IsTrue(dto.Name is not null);
        Assert.IsTrue(dto.Value is not null);
    }

    private class TestModel
    {
        public required int Id { get; set; }

        [DynDtoName(nameof(Name))]
        public required string Name { get; set; }

        //[DynDtoName(nameof(Value))]
        public required object Value { get; set; }
    }

    private class TestDto
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public object Value { get; set; } = (15, "test");
    }
}