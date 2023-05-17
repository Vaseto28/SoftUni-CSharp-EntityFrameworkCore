using System.Xml.Serialization;

namespace ProductShop.DTOs.Import;

[XmlType("Category")]
public class ImportCategoriesDTO
{
	[XmlElement("name")]
	public string Name { get; set; } = null!;
}

