using CuteUtils.Misc;

namespace CuteUtils.Tests.Misc;

[TestClass]
public class XmlHelperTests
{
    public class TestModel
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public NestedModel? Child { get; set; }
    }

    public class NestedModel
    {
        public string? Value { get; set; }
    }

    [TestMethod]
    public void SerializeDeserialize_RoundTrip_RetainsData()
    {
        TestModel model = new TestModel
        {
            Name = "Alice",
            Age = 30,
            Child = new NestedModel { Value = "Inner" }
        };

        string xml = XmlHelper.Serialize(model);
        Assert.IsFalse(string.IsNullOrWhiteSpace(xml), "Serialized XML should not be empty.");
        Assert.Contains("Alice", xml, "XML should contain the Name value.");
        Assert.Contains("30", xml, "XML should contain the Age value.");
        Assert.Contains("Inner", xml, "XML should contain the Nested Value.");

        TestModel? deserialized = XmlHelper.Deserialize<TestModel>(xml);
        Assert.IsNotNull(deserialized, "Deserialized object should not be null.");
        Assert.AreEqual(model.Name, deserialized!.Name);
        Assert.AreEqual(model.Age, deserialized.Age);
        Assert.AreEqual(model.Child?.Value, deserialized.Child?.Value);
    }

    [TestMethod]
    public void Serialize_OmitsNamespaceDeclarations()
    {
        TestModel model = new TestModel { Name = "Bob", Age = 25 };
        string xml = XmlHelper.Serialize(model);

        // Expect no namespace attributes like xmlns or common schema prefixes
        Assert.DoesNotContain("xmlns", xml, "XML should not contain namespace declarations.");
        Assert.DoesNotContain("xsi:", xml, "XML should not contain xsi prefix.");
        Assert.DoesNotContain("xsd:", xml, "XML should not contain xsd prefix.");
    }

    [TestMethod]
    public void Deserialize_NullXml_ThrowsArgumentNullException()
    {
        _ = Assert.ThrowsExactly<ArgumentNullException>(() => XmlHelper.Deserialize<TestModel>(null!));
    }

    [TestMethod]
    public void GetXmlSchema_ReturnsSchemaContainingTypeName()
    {
        string schema = XmlHelper.GetXmlSchema<TestModel>();
        Assert.IsFalse(string.IsNullOrWhiteSpace(schema), "Schema should not be empty.");
        Assert.IsGreaterThanOrEqualTo(0, schema.IndexOf("schema", StringComparison.OrdinalIgnoreCase), "Schema should contain 'schema' element.");
        Assert.Contains("TestModel", schema, "Schema should reference the TestModel type.");
    }
}