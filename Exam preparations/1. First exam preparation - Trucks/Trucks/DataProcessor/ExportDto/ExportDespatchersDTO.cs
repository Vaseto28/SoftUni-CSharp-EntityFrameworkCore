using System.Xml.Serialization;

namespace Trucks.DataProcessor.ExportDto;

[XmlType("Despatcher")]
public class ExportDespatchersDTO
{
	[XmlAttribute("TrucksCount")]
	public int TrucksCount { get; set; }

	[XmlElement("DespatcherName")]
	public string DespatcherName { get; set; } = null!;

	[XmlArray("Trucks")]
	public ExportDespatcherTrucksDTO[] Trucks { get; set; } = null!;
}

