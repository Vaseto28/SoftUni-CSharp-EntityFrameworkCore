using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto;

[XmlType("Truck")]
public class ExportDespatcherTrucksDTO
{
	[XmlElement("RegistrationNumber")]
	public string RegistrationNumber { get; set; } = null!;

	[XmlElement("Make")]
	public string Make { get; set; } = null!;
}

