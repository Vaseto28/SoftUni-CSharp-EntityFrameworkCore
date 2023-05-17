using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

[XmlType("User")]
public class ExportUsersDTO
{
	[XmlElement("firstName")]
	public string FirstName { get; set; } = null!;

	[XmlElement("lastName")]
	public string LastName { get; set; } = null!;

	[XmlElement("soldProducts")]
	public ICollection<ExportSoldProductsDTO> Products { get; set; } = null!;
}

