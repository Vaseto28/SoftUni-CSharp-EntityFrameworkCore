using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Utilities;

namespace Footballers.DataProcessor.ImportDto;

[XmlType("Coach")]
public class ImportCoachesDTO
{
	[XmlElement("Name")]
	[Required]
	[MinLength(ValidationConstraints.CoachNameMinLength)]
	[MaxLength(ValidationConstraints.CoachNameMaxLength)]
	public string Name { get; set; } = null!;

	[XmlElement("Nationality")]
	[Required]
	public string Nationality { get; set; } = null!;

	[XmlArray("Footballers")]
	public ImportFootballersDTO[] Footballers { get; set; } = null!;
}

