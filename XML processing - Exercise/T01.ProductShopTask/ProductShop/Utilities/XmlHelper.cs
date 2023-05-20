using ProductShop.DTOs.Import;
using System.Text;
using System.Xml.Serialization;

namespace ProductShop.Utilities;

public class XmlHelper
{
	public T Deserialize<T>(string rootName, string inputXml)
	{
        XmlRootAttribute root = new XmlRootAttribute(rootName);

        XmlSerializer serializer = new XmlSerializer(typeof(T), root);

        using StringReader reader = new StringReader(inputXml);

        T users = (T)serializer.Deserialize(reader);

        return users;
    }

    public string Serialize<T>(T obj, string rootName)
    {
        StringBuilder sb = new StringBuilder();

        XmlRootAttribute root = new XmlRootAttribute(rootName);

        XmlSerializer serializer = new XmlSerializer(typeof(T), root);

        using StringWriter writer = new StringWriter(sb);

        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
        namespaces.Add(String.Empty, String.Empty);

        serializer.Serialize(writer, obj, namespaces);

        return sb.ToString().Trim();
    }
}

