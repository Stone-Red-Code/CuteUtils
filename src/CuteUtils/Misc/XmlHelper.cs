using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace CuteUtils.Misc;

/// <summary>
/// Provides simple XML serialization and deserialization helpers and schema generation helpers.
/// </summary>
public static class XmlHelper
{
    /// <summary>
    /// Deserializes an XML string into an instance of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The target reference type to deserialize into. Must be a class.</typeparam>
    /// <param name="xml">The XML string to deserialize. Must not be null.</param>
    /// <param name="settings">Optional <see cref="XmlReaderSettings"/> to control reader behavior. If null, default settings are used.</param>
    /// <returns>An instance of <typeparamref name="T"/> if deserialization succeeds; otherwise, <c>null</c>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="xml"/> is null.</exception>
    /// <exception cref="InvalidOperationException">May be thrown by <see cref="XmlSerializer"/> when deserialization fails.</exception>
    public static T? Deserialize<T>(string xml, XmlReaderSettings? settings = null) where T : class
    {
        ArgumentNullException.ThrowIfNull(xml);

        XmlSerializer serializer = new XmlSerializer(typeof(T));

        using StringReader stringReader = new StringReader(xml);
        using XmlReader xmlReader = XmlReader.Create(stringReader, settings);

        return serializer.Deserialize(xmlReader) as T;
    }

    /// <summary>
    /// Serializes an object to its XML representation.
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize.</typeparam>
    /// <param name="obj">The object instance to serialize. Must not be null.</param>
    /// <param name="settings">Optional <see cref="XmlWriterSettings"/> to control writer behavior. If null, default settings are used.</param>
    /// <param name="xmlSerializerNamespaces">Optional <see cref="XmlSerializerNamespaces"/> to control namespace declarations. If null, an empty namespace is used to omit declarations.</param>
    /// <returns>A string containing the XML representation of <paramref name="obj"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="obj"/> is null.</exception>
    /// <exception cref="InvalidOperationException">May be thrown by <see cref="XmlSerializer"/> when serialization fails.</exception>
    public static string Serialize<T>(T obj, XmlWriterSettings? settings = null, XmlSerializerNamespaces? xmlSerializerNamespaces = null)
    {
        ArgumentNullException.ThrowIfNull(obj);

        settings ??= new XmlWriterSettings();

        if (xmlSerializerNamespaces == null)
        {
            xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add(string.Empty, string.Empty);
        }

        XmlSerializer xmlSerializer = new(typeof(T));

        // Omit namespace declarations by adding an empty namespace
        xmlSerializerNamespaces.Add(string.Empty, string.Empty);

        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);

        xmlSerializer.Serialize(xmlWriter, obj, xmlSerializerNamespaces);
        return stringWriter.ToString();
    }

    /// <summary>
    /// Generates XML schema definitions for the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type for which to generate XML schema.</typeparam>
    /// <param name="settings">Optional <see cref="XmlWriterSettings"/> to control output formatting. If null, default settings are used.</param>
    /// <returns>A string containing one or more XML schema documents representing <typeparamref name="T"/>.</returns>
    /// <exception cref="InvalidOperationException">May be thrown when schema export fails.</exception>
    public static string GetXmlSchema<T>(XmlWriterSettings? settings = null)
    {
        settings ??= new XmlWriterSettings();

        XmlSchemas schemas = [];
        XmlSchemaExporter exporter = new XmlSchemaExporter(schemas);
        XmlTypeMapping mapping = new XmlReflectionImporter().ImportTypeMapping(typeof(T));

        exporter.ExportTypeMapping(mapping);

        using StringWriter stringWriter = new StringWriter();
        using XmlWriter xmlWriter = XmlWriter.Create(stringWriter, settings);

        foreach (XmlSchema schema in schemas)
        {
            schema.Write(xmlWriter);
        }

        return stringWriter.ToString();
    }
}