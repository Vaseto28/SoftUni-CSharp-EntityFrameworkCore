using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using Artillery.Common;

namespace Artillery.DataProcessor.ImportDto;

[XmlType("Manufacturer")]
public class ImportManufacturersDTO
{
	[XmlElement("ManufacturerName")]
	[Required]
	[MinLength(ValidationConstraints.ManufacturerNameMinLength)]
	[MaxLength(ValidationConstraints.ManufacturerNameMaxLength)]
	public string ManufacturerName { get; set; } = null!;

	[XmlElement("Founded")]
	[Required]
	[MinLength(ValidationConstraints.ManufacturerFoundedMinLength)]
	[MaxLength(ValidationConstraints.ManufacturerFoundedMaxLength)]
	public string Founded { get; set; } = null!;
}

