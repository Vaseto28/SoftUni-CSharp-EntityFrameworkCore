using System.Xml.Serialization;

namespace Theatre.DataProcessor.ExportDto;

[XmlType("Play")]
public class ExportPlaysDTO
{
	[XmlAttribute("Title")]
	public string Title { get; set; } = null!;

	[XmlAttribute("Duration")]
	public string Duration { get; set; } = null!;

	[XmlAttribute("Rating")]
	public float Rating { get; set; }

	[XmlAttribute("Genre")]
	public string Genre { get; set; } = null!;

	[XmlArray("Actors")]
	public ExportCastDTO[]? Actors { get; set; }
}

