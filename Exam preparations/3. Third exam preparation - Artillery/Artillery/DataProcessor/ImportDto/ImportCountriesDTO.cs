using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Common;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Country")]
public class ImportCountriesDTO
{
	[XmlElement("CountryName")]
	[Required]
	[MinLength(ValidationConstraints.CountryNameMinLength)]
	[MaxLength(ValidationConstraints.CountryNameMaxLength)]
	public string CountryName { get; set; } = null!;

	[XmlElement("ArmySize")]
	[Required]
	[Range(ValidationConstraints.CountryArmyMinSize, ValidationConstraints.CountryArmyMaxSize)]
	public int ArmySize { get; set; }
}

