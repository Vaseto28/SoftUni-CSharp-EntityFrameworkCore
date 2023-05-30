using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Footballers.Data.Models.Enums;
using Footballers.Utilities;

namespace Footballers.DataProcessor.ImportDto;

[XmlType("Footballer")]
public class ImportFootballersDTO
{
	[Required]
	[MinLength(ValidationConstraints.FootballerNameMinLength)]
	[MaxLength(ValidationConstraints.FootballerNameMaxLength)]
	public string Name { get; set; } = null!;

	[XmlElement("ContractStartDate")]
	[Required]
	public string ContractStartDate { get; set; } = null!;

	[XmlElement("ContractEndDate")]
	[Required]
	public string ContractEndDate { get; set; } = null!;

	[XmlElement("BestSkillType")]
	[Required]
	[Range(0, 4)]
	public int BestSkillType { get; set; }

	[XmlElement("PositionType")]
	[Required]
	[Range(0, 3)]
	public int PositionType { get; set; }
}

