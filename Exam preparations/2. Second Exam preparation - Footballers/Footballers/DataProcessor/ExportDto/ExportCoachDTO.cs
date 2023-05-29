using System.Xml.Serialization;

namespace Footballers.DataProcessor.ExportDto;

[XmlType("Coach")]
public class ExportCoachDTO
{
	[XmlElement("CoachName")]
	public string Name { get; set; } = null!;

	[XmlArray("Footballers")]
	public ExportFootballersByCoachesDTO[] Footballers { get; set; } = null!;

	[XmlAttribute("FootballersCount")]
	public int FootballersCount { get; set; }
}

